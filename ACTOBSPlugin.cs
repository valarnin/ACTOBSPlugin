using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Advanced_Combat_Tracker;
using OBSWebsocketDotNet;
using OBSWebsocketDotNet.Communication;

namespace ACTOBSPlugin
{
    public class ACTOBSPlugin : IActPluginV1
    {
        private OBSWebsocket obs;

        private LogLineEventDelegate LogLineDel;

        private CombatToggleEventDelegate OnCombatEndDel;

        private readonly PluginConfig config = new PluginConfig();

        // We work around concurrency issues here by just using `ToList` on these collections whenever iterating over them
        // Because changes to the structure are only initialized on init and on the UI thread, we don't need to lock
        private readonly List<Regex> startRecordingRegexes = new List<Regex>();
        private readonly List<Regex> stopRecordingRegexes = new List<Regex>();
        private ConfigPanel configPanel;

        private string lastVidFile = "";
        private bool isConnected = false;
        private bool isRecording = false;

        private string GetPluginDirectory()
        {
            var plugin = ActGlobals.oFormActMain.ActPlugins.Where(x => x.pluginObj == this).FirstOrDefault();
            if (plugin != null)
            {
                return Path.GetDirectoryName(plugin.pluginFile.FullName);
            }
            else
            {
                throw new Exception("Could not find ourselves in the plugin list!");
            }
        }

        private void TryConnect()
        {
            if (config.Enabled)
            {
                try
                {
                    obs.ConnectAsync(config.IPPort, config.Password);
                }
                catch (Exception ex)
                {
                    ActGlobals.oFormActMain.BeginInvoke((MethodInvoker)delegate
                    {
                        MessageBox.Show("Connect failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    });
                }
            }
        }

        private void Disconnect()
        {
            obs.Disconnect();
        }

        private void Obs_Disconnected(object sender, ObsDisconnectionInfo e)
        {
            isConnected = false;
            UpdateStatus();
            Task.Delay(5000).ContinueWith(_ => {
                TryConnect();
            });
        }

        private void Obs_Connected(object sender, EventArgs e)
        {
            isConnected = true;
            UpdateStatus();
        }

        public void DeInitPlugin()
        {
            obs.Connected -= Obs_Connected;
            obs.Disconnected -= Obs_Disconnected;
            Disconnect();
            ActGlobals.oFormActMain.OnCombatEnd -= OnCombatEndDel;
            ActGlobals.oFormActMain.BeforeLogLineRead -= LogLineDel;
            config.Save();
        }

        private void RebuildRegexes(bool start, bool stop)
        {
            if (start)
            {
                startRecordingRegexes.Clear();
                foreach (var txtRegex in config.StartRecording)
                {
                    try
                    {
                        startRecordingRegexes.Add(new Regex(txtRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled));
                    }
                    catch (Exception e)
                    {
                        ActGlobals.oFormActMain.WriteExceptionLog(e, "Exception parsing regex " + txtRegex);
                    }
                }
            }
            if (stop)
            {
                stopRecordingRegexes.Clear();
                foreach (var txtRegex in config.StopRecording)
                {
                    try
                    {
                        stopRecordingRegexes.Add(new Regex(txtRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled));
                    }
                    catch (Exception e)
                    {
                        ActGlobals.oFormActMain.WriteExceptionLog(e, "Exception parsing regex " + txtRegex);
                    }
                }
            }
        }

        public void InitPlugin(TabPage pluginScreenSpace, Label pluginStatusText)
        {
            var dir = GetPluginDirectory();
            var autoLoadAssemblies = new string[] {
                "Newtonsoft.Json.dll",
                "System.Reactive.dll",
                "System.Threading.Channels.dll",
                "System.Threading.Tasks.Extensions.dll",
                "Websocket.Client.dll",
                "obs-websocket-dotnet.dll"
            };
            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assemblyFilename in autoLoadAssemblies)
            {
                if (currentAssemblies.Any(a => {
                    if (a.IsDynamic) return false;
                    var manifest = a.ManifestModule;
                    if (manifest.ScopeName.Equals(assemblyFilename)) return true;
                    return false;
                    }))
                {
                    continue;
                }
                AppDomain.CurrentDomain.Load(File.ReadAllBytes(Path.Combine(dir, assemblyFilename)));
            }

            pluginStatusText.Text = "Loading PluginConfig";
            config.Load();
            RebuildRegexes(true, true);
            config.ConfigChanged += (_, args) => {
                RebuildRegexes(args.StartRecordingChanged, args.StopRecordingChanged);
                if (args.EnabledChanged)
                {
                    if (config.Enabled)
                    {
                        TryConnect();
                    }
                    else
                    {
                        Disconnect();
                    }
                }
            };

            pluginStatusText.Text = "Creating ConfigPanel";
            pluginScreenSpace.Text = "ACT OBS Plugin";
            configPanel = new ConfigPanel(config);
            pluginScreenSpace.Controls.Add(configPanel);
            pluginStatusText.Text = "In InitPlugin()";

            PrivateInit();
        }

        private void PrivateInit()
        {
            obs = new OBSWebsocket();
            obs.Connected += Obs_Connected;
            obs.Disconnected += Obs_Disconnected;
            TryConnect();

            LogLineDel = (bool isImport, LogLineEventArgs logInfo) =>
            {
                try
                {
                    var line = logInfo.originalLogLine;
                    if (obs.IsConnected)
                    {
                        if (!obs.GetRecordStatus().IsRecording)
                        {
                            foreach (var re in startRecordingRegexes)
                            {
                                if (re.IsMatch(line))
                                {
                                    isRecording = true;
                                    obs.StartRecord();
                                    UpdateStatus();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            foreach (var re in stopRecordingRegexes)
                            {
                                if (re.IsMatch(line))
                                {
                                    isRecording = false;
                                    // Get this info before calling `StopRecord` because it could change due to delay in stopping recording process
                                    var currentEnc = ActGlobals.oFormActMain.ActiveZone.ActiveEncounter;
                                    var currentZone = ActGlobals.oFormActMain.ActiveZone.ZoneName;
                                    var vidFile = obs.StopRecord();
                                    if (config.AutoRename)
                                    {
                                        Task.Delay(5000).ContinueWith(_ =>
                                        {
                                            var encTitle = currentEnc.Title;
                                            var baseFilename = Path.GetFileNameWithoutExtension(vidFile);
                                            var zoneEnc = string.Join("_", (currentZone + "_" + encTitle).Split(Path.GetInvalidFileNameChars(), StringSplitOptions.RemoveEmptyEntries)).TrimEnd('.');
                                            var extension = Path.GetExtension(vidFile);
                                            var newFileName = baseFilename + "_" + zoneEnc + extension;
                                            var renamedFile = Path.Combine(
                                                Path.GetDirectoryName(vidFile),
                                                newFileName
                                                );
                                            File.Move(vidFile, renamedFile);
                                            lastVidFile = newFileName;
                                            UpdateStatus();
                                        });
                                    }
                                    else
                                    {
                                        UpdateStatus();
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception: " + ex.ToString());
                }
            };

            OnCombatEndDel = (_, encounterInfo) =>
            {
                return;
            };

            ActGlobals.oFormActMain.OnCombatEnd += OnCombatEndDel;

            ActGlobals.oFormActMain.BeforeLogLineRead += LogLineDel;
        }

        private void UpdateStatus()
        {
            var status = "";

            if (isConnected)
            {
                status += "Connected, ";
                if (isRecording)
                {
                    status += "Recording";
                }
                else
                {
                    status += "Not Recording";
                }
            }
            else
            {
                status += "Disconnected, recording status unknown";
            }

            if (ActGlobals.oFormActMain.InvokeRequired)
            {
                ActGlobals.oFormActMain.Invoke((Action)(() => {
                    configPanel.SetStatus(status);
                    configPanel.SetLastFile(lastVidFile);
                }));
            }
            else
            {
                configPanel.SetStatus(status);
                configPanel.SetLastFile(lastVidFile);
            }
        }
    }
}
