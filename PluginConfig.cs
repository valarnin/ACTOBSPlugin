using Advanced_Combat_Tracker;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ACTOBSPlugin
{
    public class PluginConfig
    {
        private bool enabled = false;
        private bool autoRename = false;
        private string hostPort = "ws://127.0.0.1:4455";
        private string password = "";
        private string[] startRecording = new string[] { };
        private string[] stopRecording = new string[] { };
        private string settingsFilePath = Path.Combine(ActGlobals.oFormActMain.AppDataFolder.FullName, "Config\\ACTOBSPlugin.config.json");

        private bool loaded = false;

        public bool Enabled
        {
            get => enabled;
            set {
                var changed = enabled != value;
                enabled = value;
                FireEvent(EnabledChanged: changed);
            }
        }
        public bool AutoRename
        {
            get => autoRename;
            set {
                var changed = autoRename != value;
                autoRename = value;
                FireEvent(AutoRenameChanged: changed);
            }
        }
        public string IPPort
        {
            get => hostPort;
            set {
                var changed = hostPort != value;
                hostPort = value;
                FireEvent(HostPortChanged: changed);
            }
        }
        public string Password
        {
            get => password;
            set {
                var changed = password != value;
                password = value;
                FireEvent(PasswordChanged: changed);
            }
        }
        public string[] StartRecording
        {
            get => startRecording;
            set {
                var changed = string.Join("\n", startRecording) != string.Join("\n", value);
                startRecording = value;
                FireEvent(StartRecordingChanged: changed);
            }
        }
        public string[] StopRecording
        {
            get => stopRecording;
            set {
                var changed = string.Join("\n", stopRecording) != string.Join("\n", value);
                stopRecording = value;
                FireEvent(StopRecordingChanged: changed);
            }
        }

        public event EventHandler<PluginConfigEventChangedArgs> ConfigChanged;

        public void Load()
        {
            loaded = true;
            try
            {
                var loadedConfig = JsonConvert.DeserializeObject<PluginConfig>(File.ReadAllText(settingsFilePath));
                enabled = loadedConfig.enabled;
                autoRename = loadedConfig.autoRename;
                hostPort = loadedConfig.hostPort;
                password = loadedConfig.password;
                startRecording = loadedConfig.startRecording;
                stopRecording = loadedConfig.stopRecording;
            }
            catch (Exception ex)
            {
                ActGlobals.oFormActMain.WriteExceptionLog(ex, "Exception loading ACTOBSPlugin config");
            }
        }
        public void Save()
        {
            var json = JsonConvert.SerializeObject(this);
            File.WriteAllText(settingsFilePath, json);
        }

        private void FireEvent(bool EnabledChanged = false, bool AutoRenameChanged = false, bool HostPortChanged = false, bool PasswordChanged = false, bool StartRecordingChanged = false, bool StopRecordingChanged = false)
        {
            if (!loaded) return;

            if (EnabledChanged || AutoRenameChanged || HostPortChanged || PasswordChanged || StartRecordingChanged || StopRecordingChanged)
            {
                Save();
                ConfigChanged.Invoke(this, new PluginConfigEventChangedArgs(EnabledChanged, AutoRenameChanged, HostPortChanged, PasswordChanged, StartRecordingChanged, StopRecordingChanged));
            }
        }

        public class PluginConfigEventChangedArgs
        {
            public PluginConfig Config;
            public bool EnabledChanged;
            public bool AutoRenameChanged;
            public bool HostPortChanged;
            public bool PasswordChanged;
            public bool StartRecordingChanged;
            public bool StopRecordingChanged;

            public PluginConfigEventChangedArgs(bool enabledChanged, bool autoRenameChanged, bool hostPortChanged, bool passwordChanged, bool startRecordingChanged, bool stopRecordingChanged)
            {
                EnabledChanged = enabledChanged;
                AutoRenameChanged = autoRenameChanged;
                HostPortChanged = hostPortChanged;
                PasswordChanged = passwordChanged;
                StartRecordingChanged = startRecordingChanged;
                StopRecordingChanged = stopRecordingChanged;
            }
        }
    }
}
