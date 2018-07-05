namespace hactool__
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.InputFileLabel = new System.Windows.Forms.Label();
            this.OutputOptionsLabel = new System.Windows.Forms.Label();
            this.TitleKeyLabel = new System.Windows.Forms.Label();
            this.Header = new System.Windows.Forms.CheckBox();
            this.OnlyUpdated = new System.Windows.Forms.CheckBox();
            this.Plaintext = new System.Windows.Forms.CheckBox();
            this.InputFile = new System.Windows.Forms.TextBox();
            this.Open = new System.Windows.Forms.Button();
            this.OpenKeys = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OpenKeysDialog = new System.Windows.Forms.OpenFileDialog();
            this.OpenFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.TitleKey = new System.Windows.Forms.HexBox();
            this.hactoolProgress = new System.Windows.Forms.ProgressBar();
            this.backgroundHactool = new System.ComponentModel.BackgroundWorker();
            this.UnpackingLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // InputFileLabel
            // 
            this.InputFileLabel.AutoSize = true;
            this.InputFileLabel.BackColor = System.Drawing.Color.Gray;
            this.InputFileLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.InputFileLabel.ForeColor = System.Drawing.Color.White;
            this.InputFileLabel.Location = new System.Drawing.Point(3, 16);
            this.InputFileLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.InputFileLabel.Name = "InputFileLabel";
            this.InputFileLabel.Size = new System.Drawing.Size(38, 13);
            this.InputFileLabel.TabIndex = 16;
            this.InputFileLabel.Text = "Input:";
            // 
            // OutputOptionsLabel
            // 
            this.OutputOptionsLabel.AutoSize = true;
            this.OutputOptionsLabel.BackColor = System.Drawing.Color.Gray;
            this.OutputOptionsLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.OutputOptionsLabel.ForeColor = System.Drawing.Color.White;
            this.OutputOptionsLabel.Location = new System.Drawing.Point(6, 74);
            this.OutputOptionsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.OutputOptionsLabel.Name = "OutputOptionsLabel";
            this.OutputOptionsLabel.Size = new System.Drawing.Size(93, 13);
            this.OutputOptionsLabel.TabIndex = 19;
            this.OutputOptionsLabel.Text = "Output Options:";
            // 
            // TitleKeyLabel
            // 
            this.TitleKeyLabel.AutoSize = true;
            this.TitleKeyLabel.BackColor = System.Drawing.Color.Gray;
            this.TitleKeyLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.TitleKeyLabel.ForeColor = System.Drawing.Color.White;
            this.TitleKeyLabel.Location = new System.Drawing.Point(4, 46);
            this.TitleKeyLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TitleKeyLabel.Name = "TitleKeyLabel";
            this.TitleKeyLabel.Size = new System.Drawing.Size(48, 13);
            this.TitleKeyLabel.TabIndex = 29;
            this.TitleKeyLabel.Text = "Titlekey:";
            // 
            // Header
            // 
            this.Header.AccessibleDescription = "Do you want the header as a seperate output file?";
            this.Header.AccessibleName = "OutputHeader";
            this.Header.AutoSize = true;
            this.Header.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header.ForeColor = System.Drawing.Color.White;
            this.Header.Location = new System.Drawing.Point(112, 92);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(84, 17);
            this.Header.TabIndex = 5;
            this.Header.Text = "Header File";
            this.Header.UseVisualStyleBackColor = true;
            this.Header.Click += new System.EventHandler(this.Header_Click);
            // 
            // OnlyUpdated
            // 
            this.OnlyUpdated.AccessibleDescription = "Only output the updates not the base";
            this.OnlyUpdated.AccessibleName = "OutputOnlyUpdated";
            this.OnlyUpdated.AutoSize = true;
            this.OnlyUpdated.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OnlyUpdated.ForeColor = System.Drawing.Color.White;
            this.OnlyUpdated.Location = new System.Drawing.Point(10, 115);
            this.OnlyUpdated.Name = "OnlyUpdated";
            this.OnlyUpdated.Size = new System.Drawing.Size(98, 17);
            this.OnlyUpdated.TabIndex = 6;
            this.OnlyUpdated.Text = "Only Updated";
            this.OnlyUpdated.UseVisualStyleBackColor = true;
            this.OnlyUpdated.Click += new System.EventHandler(this.OnlyUpdated_Click);
            // 
            // Plaintext
            // 
            this.Plaintext.AccessibleDescription = "Do you want a decrypted copy of the NCA file?";
            this.Plaintext.AccessibleName = "OutputPlaintext";
            this.Plaintext.AutoSize = true;
            this.Plaintext.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Plaintext.ForeColor = System.Drawing.Color.White;
            this.Plaintext.Location = new System.Drawing.Point(10, 92);
            this.Plaintext.Name = "Plaintext";
            this.Plaintext.Size = new System.Drawing.Size(91, 17);
            this.Plaintext.TabIndex = 4;
            this.Plaintext.Text = "Plaintext File";
            this.Plaintext.UseVisualStyleBackColor = true;
            this.Plaintext.Click += new System.EventHandler(this.Plaintext_Click);
            // 
            // InputFile
            // 
            this.InputFile.AccessibleDescription = "You can paste a file location in here";
            this.InputFile.AccessibleName = "InputFile";
            this.InputFile.AllowDrop = true;
            this.InputFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.InputFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InputFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputFile.Location = new System.Drawing.Point(67, 15);
            this.InputFile.Margin = new System.Windows.Forms.Padding(2);
            this.InputFile.Name = "InputFile";
            this.InputFile.Size = new System.Drawing.Size(293, 16);
            this.InputFile.TabIndex = 1;
            // 
            // Open
            // 
            this.Open.AccessibleDescription = "Opens a File Dialog if clicked or if holding the shift key, will open a folder fo" +
    "r bulk input";
            this.Open.AccessibleName = "OpenFileorFolder";
            this.Open.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Open.FlatAppearance.BorderSize = 0;
            this.Open.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.Open.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Open.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Open.ForeColor = System.Drawing.Color.White;
            this.Open.Location = new System.Drawing.Point(375, 15);
            this.Open.Margin = new System.Windows.Forms.Padding(2);
            this.Open.Name = "Open";
            this.Open.Size = new System.Drawing.Size(98, 46);
            this.Open.TabIndex = 3;
            this.Open.Text = "Open File";
            this.Open.UseVisualStyleBackColor = false;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // OpenKeys
            // 
            this.OpenKeys.AccessibleDescription = "For selecting a keys file";
            this.OpenKeys.AccessibleName = "OpenKeys";
            this.OpenKeys.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.OpenKeys.FlatAppearance.BorderSize = 0;
            this.OpenKeys.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.OpenKeys.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenKeys.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenKeys.ForeColor = System.Drawing.Color.White;
            this.OpenKeys.Location = new System.Drawing.Point(219, 100);
            this.OpenKeys.Margin = new System.Windows.Forms.Padding(2);
            this.OpenKeys.Name = "OpenKeys";
            this.OpenKeys.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.OpenKeys.Size = new System.Drawing.Size(120, 39);
            this.OpenKeys.TabIndex = 7;
            this.OpenKeys.Text = "Open Keys";
            this.OpenKeys.UseVisualStyleBackColor = false;
            this.OpenKeys.Click += new System.EventHandler(this.OpenKeys_Click);
            // 
            // Start
            // 
            this.Start.AccessibleDescription = "Start the un-packing process";
            this.Start.AccessibleName = "Start";
            this.Start.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Start.Enabled = false;
            this.Start.FlatAppearance.BorderSize = 0;
            this.Start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Start.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.ForeColor = System.Drawing.Color.White;
            this.Start.Location = new System.Drawing.Point(348, 100);
            this.Start.Margin = new System.Windows.Forms.Padding(2);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(125, 39);
            this.Start.TabIndex = 8;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.DefaultExt = "nca";
            this.OpenFileDialog.Filter = "Switch Files|*.nca;*.xci;*.nsp|All Files (*.*)|*.*";
            this.OpenFileDialog.InitialDirectory = "D:\\GITHUB\\hactool--";
            this.OpenFileDialog.Title = "Select a File...";
            // 
            // OpenKeysDialog
            // 
            this.OpenKeysDialog.DefaultExt = "keys";
            this.OpenKeysDialog.Filter = "Keys File|*.keys;*.txt|All Files (*.*)|*.*";
            this.OpenKeysDialog.InitialDirectory = "D:\\GITHUB\\hactool--";
            this.OpenKeysDialog.Title = "Select a Keys File...";
            // 
            // TitleKey
            // 
            this.TitleKey.AccessibleDescription = "Enter the 16 byte title key here";
            this.TitleKey.AccessibleName = "TitleKey";
            this.TitleKey.AllowDrop = true;
            this.TitleKey.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TitleKey.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TitleKey.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleKey.Location = new System.Drawing.Point(67, 45);
            this.TitleKey.Margin = new System.Windows.Forms.Padding(2);
            this.TitleKey.MaxLength = 32;
            this.TitleKey.Name = "TitleKey";
            this.TitleKey.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TitleKey.Size = new System.Drawing.Size(293, 16);
            this.TitleKey.TabIndex = 2;
            this.TitleKey.WordWrap = false;
            // 
            // hactoolProgress
            // 
            this.hactoolProgress.AccessibleDescription = "Overall progress of the extraction";
            this.hactoolProgress.AccessibleName = "hactoolProgress";
            this.hactoolProgress.Location = new System.Drawing.Point(375, 69);
            this.hactoolProgress.Name = "hactoolProgress";
            this.hactoolProgress.Size = new System.Drawing.Size(97, 23);
            this.hactoolProgress.TabIndex = 30;
            this.hactoolProgress.Visible = false;
            // 
            // backgroundHactool
            // 
            this.backgroundHactool.WorkerReportsProgress = true;
            this.backgroundHactool.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundHactool_DoWork);
            this.backgroundHactool.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundHactool_RunWorkerCompleted);
            // 
            // UnpackingLabel
            // 
            this.UnpackingLabel.AutoSize = true;
            this.UnpackingLabel.BackColor = System.Drawing.Color.Gray;
            this.UnpackingLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.UnpackingLabel.ForeColor = System.Drawing.Color.White;
            this.UnpackingLabel.Location = new System.Drawing.Point(233, 74);
            this.UnpackingLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.UnpackingLabel.Name = "UnpackingLabel";
            this.UnpackingLabel.Size = new System.Drawing.Size(137, 13);
            this.UnpackingLabel.TabIndex = 31;
            this.UnpackingLabel.Text = "Unpacking... Please Wait!";
            this.UnpackingLabel.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(485, 150);
            this.Controls.Add(this.UnpackingLabel);
            this.Controls.Add(this.hactoolProgress);
            this.Controls.Add(this.InputFileLabel);
            this.Controls.Add(this.OutputOptionsLabel);
            this.Controls.Add(this.TitleKeyLabel);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.OnlyUpdated);
            this.Controls.Add(this.Plaintext);
            this.Controls.Add(this.TitleKey);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.InputFile);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.OpenKeys);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "hactool++";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion

        #region Variables
        internal System.Windows.Forms.Label InputFileLabel;
        internal System.Windows.Forms.Label OutputOptionsLabel;
        internal System.Windows.Forms.Label TitleKeyLabel;
        internal System.Windows.Forms.Label UnpackingLabel;
        internal System.Windows.Forms.TextBox InputFile;
        internal System.Windows.Forms.HexBox TitleKey;
        internal System.Windows.Forms.Button Open;
        internal System.Windows.Forms.Button OpenKeys;
        internal System.Windows.Forms.Button Start;
        private System.Windows.Forms.CheckBox Header;
        private System.Windows.Forms.CheckBox OnlyUpdated;
        private System.Windows.Forms.CheckBox Plaintext;
        private System.Windows.Forms.FolderBrowserDialog OpenFolderDialog;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
        private System.Windows.Forms.OpenFileDialog OpenKeysDialog;
        private System.Windows.Forms.ProgressBar hactoolProgress;
        private System.ComponentModel.BackgroundWorker backgroundHactool;
        #endregion 
    }
}