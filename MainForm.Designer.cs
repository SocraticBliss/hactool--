using System.Windows.Controls;
using System.Windows.Forms;

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
            this.KeyManager = new System.Windows.Forms.Button();
            this.InputFileLabel = new System.Windows.Forms.Label();
            this.Open = new System.Windows.Forms.Button();
            this.InputFileBox = new System.Windows.Forms.TextBox();
            this.OutputOptionsLabel = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.TitleKeyLabel = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openKeyManager = new System.Windows.Forms.OpenFileDialog();
            this.Plaintext = new System.Windows.Forms.CheckBox();
            this.Header = new System.Windows.Forms.CheckBox();
            this.OnlyUpdated = new System.Windows.Forms.CheckBox();
            this.TitleKeyInput = new System.Windows.Controls.HexTextBox();
            this.openFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // KeyManager
            // 
            this.KeyManager.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.KeyManager.FlatAppearance.BorderSize = 0;
            this.KeyManager.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.KeyManager.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KeyManager.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyManager.ForeColor = System.Drawing.Color.White;
            this.KeyManager.Location = new System.Drawing.Point(219, 89);
            this.KeyManager.Margin = new System.Windows.Forms.Padding(2);
            this.KeyManager.Name = "KeyManager";
            this.KeyManager.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.KeyManager.Size = new System.Drawing.Size(120, 39);
            this.KeyManager.TabIndex = 6;
            this.KeyManager.Text = "Select Keys";
            this.KeyManager.UseVisualStyleBackColor = false;
            this.KeyManager.Click += new System.EventHandler(this.KeyManager_Click);
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
            this.InputFileLabel.Size = new System.Drawing.Size(63, 13);
            this.InputFileLabel.TabIndex = 16;
            this.InputFileLabel.Text = "Input NCA:";
            // 
            // Open
            // 
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
            this.Open.TabIndex = 4;
            this.Open.Text = "Select NCA";
            this.Open.UseVisualStyleBackColor = false;
            this.Open.Click += new System.EventHandler(this.Open_Click);
            // 
            // InputFileBox
            // 
            this.InputFileBox.AllowDrop = true;
            this.InputFileBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.InputFileBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InputFileBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InputFileBox.Location = new System.Drawing.Point(67, 15);
            this.InputFileBox.Margin = new System.Windows.Forms.Padding(2);
            this.InputFileBox.Name = "InputFileBox";
            this.InputFileBox.Size = new System.Drawing.Size(293, 16);
            this.InputFileBox.TabIndex = 3;
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
            this.OutputOptionsLabel.Visible = false;
            // 
            // Start
            // 
            this.Start.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Start.FlatAppearance.BorderSize = 0;
            this.Start.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkGray;
            this.Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Start.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.ForeColor = System.Drawing.Color.White;
            this.Start.Location = new System.Drawing.Point(348, 89);
            this.Start.Margin = new System.Windows.Forms.Padding(2);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(125, 39);
            this.Start.TabIndex = 2;
            this.Start.Text = "Unpack";
            this.Start.UseVisualStyleBackColor = false;
            this.Start.Click += new System.EventHandler(this.Start_Click);
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
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "nca";
            this.openFileDialog.Filter = "NCA File (*.nca)|*.nca|All Files (*.*)|*.*";
            this.openFileDialog.Title = "Select a NCA File...";
            // 
            // openKeyManager
            // 
            this.openKeyManager.DefaultExt = "keys";
            this.openKeyManager.FileName = "prod.keys";
            this.openKeyManager.Filter = "Keys File (*.keys)|*.keys|All Files (*.*)|*.*";
            this.openKeyManager.InitialDirectory = ".";
            this.openKeyManager.Title = "Select a Keys File...";
            // 
            // Plaintext
            // 
            this.Plaintext.AutoSize = true;
            this.Plaintext.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Plaintext.ForeColor = System.Drawing.Color.White;
            this.Plaintext.Location = new System.Drawing.Point(10, 92);
            this.Plaintext.Name = "Plaintext";
            this.Plaintext.Size = new System.Drawing.Size(91, 17);
            this.Plaintext.TabIndex = 30;
            this.Plaintext.Text = "Plaintext File";
            this.Plaintext.UseVisualStyleBackColor = true;
            this.Plaintext.Visible = false;
            this.Plaintext.Click += new System.EventHandler(this.Plaintext_Click);
            // 
            // Header
            // 
            this.Header.AutoSize = true;
            this.Header.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Header.ForeColor = System.Drawing.Color.White;
            this.Header.Location = new System.Drawing.Point(112, 92);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(84, 17);
            this.Header.TabIndex = 31;
            this.Header.Text = "Header File";
            this.Header.UseVisualStyleBackColor = true;
            this.Header.Visible = false;
            this.Header.Click += new System.EventHandler(this.Header_Click);
            // 
            // OnlyUpdated
            // 
            this.OnlyUpdated.AutoSize = true;
            this.OnlyUpdated.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OnlyUpdated.ForeColor = System.Drawing.Color.White;
            this.OnlyUpdated.Location = new System.Drawing.Point(10, 115);
            this.OnlyUpdated.Name = "OnlyUpdated";
            this.OnlyUpdated.Size = new System.Drawing.Size(98, 17);
            this.OnlyUpdated.TabIndex = 32;
            this.OnlyUpdated.Text = "Only Updated";
            this.OnlyUpdated.UseVisualStyleBackColor = true;
            this.OnlyUpdated.Visible = false;
            this.OnlyUpdated.Click += new System.EventHandler(this.OnlyUpdated_Click);
            // 
            // TitleKeyInput
            // 
            this.TitleKeyInput.AllowDrop = true;
            this.TitleKeyInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TitleKeyInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TitleKeyInput.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleKeyInput.Location = new System.Drawing.Point(67, 45);
            this.TitleKeyInput.Margin = new System.Windows.Forms.Padding(2);
            this.TitleKeyInput.MaxLength = 32;
            this.TitleKeyInput.Name = "TitleKeyInput";
            this.TitleKeyInput.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TitleKeyInput.Size = new System.Drawing.Size(293, 16);
            this.TitleKeyInput.TabIndex = 1;
            this.TitleKeyInput.WordWrap = false;
            // 
            // openFolderDialog
            // 
            this.openFolderDialog.RootFolder = System.Environment.SpecialFolder.DesktopDirectory;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(485, 142);
            this.Controls.Add(this.OnlyUpdated);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.Plaintext);
            this.Controls.Add(this.TitleKeyInput);
            this.Controls.Add(this.TitleKeyLabel);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.OutputOptionsLabel);
            this.Controls.Add(this.InputFileBox);
            this.Controls.Add(this.Open);
            this.Controls.Add(this.InputFileLabel);
            this.Controls.Add(this.KeyManager);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "hactool++";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button KeyManager;
        internal System.Windows.Forms.Button Open;
        internal System.Windows.Forms.Button Start;
        internal System.Windows.Forms.Label InputFileLabel;
        internal System.Windows.Forms.Label OutputOptionsLabel;
        internal System.Windows.Forms.Label TitleKeyLabel;
        internal System.Windows.Forms.TextBox InputFileBox;
        internal HexTextBox TitleKeyInput;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.OpenFileDialog openKeyManager;
        private System.Windows.Forms.CheckBox Plaintext;
        private System.Windows.Forms.CheckBox Header;
        private System.Windows.Forms.CheckBox OnlyUpdated;
        private System.Windows.Forms.FolderBrowserDialog openFolderDialog;
    }
}

