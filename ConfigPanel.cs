using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ACTOBSPlugin
{
    public partial class ConfigPanel : UserControl
    {
        private PluginConfig config;

        private Dictionary<string, string> startRecordingRegexes = new Dictionary<string, string>() {
            // FFXIV
            { "FFXIV - ACT Combat Start", @"^(?# FFXIV - ACT Combat Start)(?<type>260)\|(?<timestamp>[^|]*)\|(?<inACTCombat>1)\|" },
            { "FFXIV - Game Combat Start", @"^(?# FFXIV - Game Combat Start)(?<type>260)\|(?<timestamp>[^|]*)\|(?<inACTCombat>[^|]*)\|(?<inGameCombat>1)\|" },
            { "FFXIV - Countdown Start", @"^(?# FFXIV - Countdown Start)(?<type>00)\|(?<timestamp>[^|]*)\|[^|]*\|[^|]*\|Battle commencing in (?<time>[^ ]+) seconds! \((?<player>.*?)\)\|" },
            { "FFXIV - Area Seal", @"^(?# FFXIV - Area Seal)(?<type>00)\|(?<timestamp>[^|]*)\|[^|]*\|[^|]*\|(?<area>.*?) will be sealed off in (?<time>[^ ]+) seconds!" },
            { "FFXIV - Engage", @"^(?# FFXIV - Countdown Start)(?<type>00)\|(?<timestamp>[^|]*)\|[^|]*\|[^|]*\|Engage!\|" },
        };

        private Dictionary<string, string> stopRecordingRegexes = new Dictionary<string, string>() {
            // FFXIV
            { "FFXIV - ACT Combat End", @"^(?# FFXIV - ACT Combat End)(?<type>260)\|(?<timestamp>[^|]*)\|(?<inACTCombat>0)\|" },
            { "FFXIV - Game Combat End", @"^(?# FFXIV - Game Combat End)(?<type>260)\|(?<timestamp>[^|]*)\|(?<inACTCombat>[^|]*)\|(?<inGameCombat>0)\|" },
            { "FFXIV - Wipe", @"^(?# FFXIV - Wipe)(?<type>33)\|(?<timestamp>[^|]*)\|(?<instance>[^|]*)\|(?<command>4000000F)\|" },
            { "FFXIV - Area Unseal", @"(?# FFXIV - Area Unseal)(?<type>00)\|(?<timestamp>[^|]*)\|[^|]*\|[^|]*\|(?<area>.*?) is no longer sealed." },
        };

        public ConfigPanel(PluginConfig config)
        {
            this.config = config;
            InitializeComponent();
            chkEnabled.Checked = config.Enabled;
            chkAutoRename.Checked = config.AutoRename;
            txtIPPort.Text = config.IPPort;
            txtPassword.Text = config.Password;
            txtStartRecording.Text = string.Join("\r\n", config.StartRecording);
            txtStopRecording.Text = string.Join("\r\n", config.StopRecording);
            cmbStartRecording.Items.AddRange(startRecordingRegexes.Keys.ToArray());
            cmbStopRecording.Items.AddRange(stopRecordingRegexes.Keys.ToArray());
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            config.Enabled = chkEnabled.Checked;
        }

        private void chkAutoRename_CheckedChanged(object sender, EventArgs e)
        {
            config.AutoRename = chkAutoRename.Checked;
        }

        private void txtIPPort_TextChanged(object sender, EventArgs e)
        {
            config.IPPort = txtIPPort.Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            config.Password = txtPassword.Text;
        }

        private void txtStartRecording_TextChanged(object sender, EventArgs e)
        {
            config.StartRecording = txtStartRecording.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void txtStopRecording_TextChanged(object sender, EventArgs e)
        {
            config.StopRecording = txtStopRecording.Text.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void btnAddStartRecording_Click(object sender, EventArgs e)
        {
            var key = cmbStartRecording.SelectedItem.ToString();
            string value;
            if (!startRecordingRegexes.TryGetValue(key, out value))
            {
                return;
            }

            if (txtStartRecording.Text.Contains(value))
            {
                return;
            }

            txtStartRecording.Text = (txtStartRecording.Text + "\r\n" + value).Trim();
        }

        private void btnAddStopRecording_Click(object sender, EventArgs e)
        {
            var key = cmbStopRecording.SelectedItem.ToString();
            string value;
            if (!stopRecordingRegexes.TryGetValue(key, out value))
            {
                return;
            }

            if (txtStopRecording.Text.Contains(value))
            {
                return;
            }

            txtStopRecording.Text = (txtStopRecording.Text + "\r\n" + value).Trim();
        }

        public void SetStatus(string status)
        {
            lblStatus.Text = "Status: " + status;
        }

        public void SetLastFile(string lastFile)
        {
            lblLastFile.Text = "Last File: " + lastFile;
        }
    }
}
