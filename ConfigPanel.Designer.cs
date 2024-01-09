
namespace ACTOBSPlugin
{
    partial class ConfigPanel
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.chkAutoRename = new System.Windows.Forms.CheckBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblIPPort = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtIPPort = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblLastFile = new System.Windows.Forms.Label();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.lblStartRecording = new System.Windows.Forms.Label();
            this.txtStartRecording = new System.Windows.Forms.TextBox();
            this.cmbStartRecording = new System.Windows.Forms.ComboBox();
            this.btnAddStartRecording = new System.Windows.Forms.Button();
            this.btnAddStopRecording = new System.Windows.Forms.Button();
            this.cmbStopRecording = new System.Windows.Forms.ComboBox();
            this.txtStopRecording = new System.Windows.Forms.TextBox();
            this.lblStopRecording = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Location = new System.Drawing.Point(4, 4);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(65, 17);
            this.chkEnabled.TabIndex = 0;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.ChkEnabled_CheckedChanged);
            // 
            // chkAutoRename
            // 
            this.chkAutoRename.AutoSize = true;
            this.chkAutoRename.Location = new System.Drawing.Point(4, 28);
            this.chkAutoRename.Name = "chkAutoRename";
            this.chkAutoRename.Size = new System.Drawing.Size(91, 17);
            this.chkAutoRename.TabIndex = 1;
            this.chkAutoRename.Text = "Auto Rename";
            this.chkAutoRename.UseVisualStyleBackColor = true;
            this.chkAutoRename.CheckedChanged += new System.EventHandler(this.ChkAutoRename_CheckedChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(101, 29);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "Password";
            // 
            // lblIPPort
            // 
            this.lblIPPort.AutoSize = true;
            this.lblIPPort.Location = new System.Drawing.Point(101, 5);
            this.lblIPPort.Name = "lblIPPort";
            this.lblIPPort.Size = new System.Drawing.Size(39, 13);
            this.lblIPPort.TabIndex = 3;
            this.lblIPPort.Text = "IP:Port";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(160, 25);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(100, 20);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.TextChanged += new System.EventHandler(this.TxtPassword_TextChanged);
            // 
            // txtIPPort
            // 
            this.txtIPPort.Location = new System.Drawing.Point(160, 3);
            this.txtIPPort.Name = "txtIPPort";
            this.txtIPPort.Size = new System.Drawing.Size(100, 20);
            this.txtIPPort.TabIndex = 5;
            this.txtIPPort.TextChanged += new System.EventHandler(this.TxtIPPort_TextChanged);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.Location = new System.Drawing.Point(266, 6);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(281, 17);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "Status: ";
            // 
            // lblLastFile
            // 
            this.lblLastFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLastFile.Location = new System.Drawing.Point(266, 28);
            this.lblLastFile.Name = "lblLastFile";
            this.lblLastFile.Size = new System.Drawing.Size(281, 17);
            this.lblLastFile.TabIndex = 7;
            this.lblLastFile.Text = "Last File: ";
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(4, 52);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(361, 26);
            this.lblInstructions.TabIndex = 8;
            this.lblInstructions.Text = "One regular expression per line. All regular expressions are case insensitive.\r\nD" +
    "o not include leading and trailing `/` characters.";
            // 
            // lblStartRecording
            // 
            this.lblStartRecording.AutoSize = true;
            this.lblStartRecording.Location = new System.Drawing.Point(7, 82);
            this.lblStartRecording.Name = "lblStartRecording";
            this.lblStartRecording.Size = new System.Drawing.Size(81, 13);
            this.lblStartRecording.TabIndex = 9;
            this.lblStartRecording.Text = "Start Recording";
            // 
            // txtStartRecording
            // 
            this.txtStartRecording.Location = new System.Drawing.Point(4, 99);
            this.txtStartRecording.Multiline = true;
            this.txtStartRecording.Name = "txtStartRecording";
            this.txtStartRecording.Size = new System.Drawing.Size(256, 421);
            this.txtStartRecording.TabIndex = 10;
            this.txtStartRecording.TextChanged += new System.EventHandler(this.TxtStartRecording_TextChanged);
            // 
            // cmbStartRecording
            // 
            this.cmbStartRecording.FormattingEnabled = true;
            this.cmbStartRecording.Location = new System.Drawing.Point(3, 526);
            this.cmbStartRecording.Name = "cmbStartRecording";
            this.cmbStartRecording.Size = new System.Drawing.Size(228, 21);
            this.cmbStartRecording.TabIndex = 11;
            // 
            // btnAddStartRecording
            // 
            this.btnAddStartRecording.Location = new System.Drawing.Point(237, 524);
            this.btnAddStartRecording.Name = "btnAddStartRecording";
            this.btnAddStartRecording.Size = new System.Drawing.Size(23, 23);
            this.btnAddStartRecording.TabIndex = 12;
            this.btnAddStartRecording.Text = "＋";
            this.btnAddStartRecording.UseVisualStyleBackColor = true;
            this.btnAddStartRecording.Click += new System.EventHandler(this.BtnAddStartRecording_Click);
            // 
            // btnAddStopRecording
            // 
            this.btnAddStopRecording.Location = new System.Drawing.Point(524, 524);
            this.btnAddStopRecording.Name = "btnAddStopRecording";
            this.btnAddStopRecording.Size = new System.Drawing.Size(23, 23);
            this.btnAddStopRecording.TabIndex = 14;
            this.btnAddStopRecording.Text = "＋";
            this.btnAddStopRecording.UseVisualStyleBackColor = true;
            this.btnAddStopRecording.Click += new System.EventHandler(this.BtnAddStopRecording_Click);
            // 
            // cmbStopRecording
            // 
            this.cmbStopRecording.FormattingEnabled = true;
            this.cmbStopRecording.Location = new System.Drawing.Point(269, 526);
            this.cmbStopRecording.Name = "cmbStopRecording";
            this.cmbStopRecording.Size = new System.Drawing.Size(249, 21);
            this.cmbStopRecording.TabIndex = 13;
            // 
            // txtStopRecording
            // 
            this.txtStopRecording.Location = new System.Drawing.Point(269, 99);
            this.txtStopRecording.Multiline = true;
            this.txtStopRecording.Name = "txtStopRecording";
            this.txtStopRecording.Size = new System.Drawing.Size(278, 421);
            this.txtStopRecording.TabIndex = 15;
            this.txtStopRecording.TextChanged += new System.EventHandler(this.TxtStopRecording_TextChanged);
            // 
            // lblStopRecording
            // 
            this.lblStopRecording.AutoSize = true;
            this.lblStopRecording.Location = new System.Drawing.Point(266, 82);
            this.lblStopRecording.Name = "lblStopRecording";
            this.lblStopRecording.Size = new System.Drawing.Size(81, 13);
            this.lblStopRecording.TabIndex = 16;
            this.lblStopRecording.Text = "Stop Recording";
            // 
            // ConfigPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.lblStopRecording);
            this.Controls.Add(this.txtStopRecording);
            this.Controls.Add(this.btnAddStopRecording);
            this.Controls.Add(this.cmbStopRecording);
            this.Controls.Add(this.btnAddStartRecording);
            this.Controls.Add(this.cmbStartRecording);
            this.Controls.Add(this.txtStartRecording);
            this.Controls.Add(this.lblStartRecording);
            this.Controls.Add(this.lblInstructions);
            this.Controls.Add(this.lblLastFile);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.txtIPPort);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblIPPort);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.chkAutoRename);
            this.Controls.Add(this.chkEnabled);
            this.Name = "ConfigPanel";
            this.Size = new System.Drawing.Size(550, 550);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.CheckBox chkAutoRename;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblIPPort;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtIPPort;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblLastFile;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.Label lblStartRecording;
        private System.Windows.Forms.TextBox txtStartRecording;
        private System.Windows.Forms.ComboBox cmbStartRecording;
        private System.Windows.Forms.Button btnAddStartRecording;
        private System.Windows.Forms.Button btnAddStopRecording;
        private System.Windows.Forms.ComboBox cmbStopRecording;
        private System.Windows.Forms.TextBox txtStopRecording;
        private System.Windows.Forms.Label lblStopRecording;
    }
}
