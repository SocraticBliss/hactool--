using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace hactool__
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            /// hactool.exe Depedency Check!
            string[] files = { "hactool.exe", "libmbedcrypto.dll", "libmbedtls.dll", "libmbedx509.dll" };
            foreach (string fileName in files)
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show(fileName + " must be in your current directory to continue!\n\n", "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }

            /// Default Keys Check!
            if (!File.Exists(defaultKeysFile))
            {
                /// If the switch home directory doesn't exist, create it!
                Directory.CreateDirectory(switchHome);

                /// Import Keys?
                if (MessageBox.Show("Do you want to import your keys?\n\n", "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (OpenKeysDialog.ShowDialog() == DialogResult.OK)
                        File.Copy(OpenKeysDialog.FileName, defaultKeysFile);
                    else
                        MessageBox.Show("Be sure to import a keys file before starting\n\n", "No default keys file", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private bool ExecuteCommand(string exeDir, string args)
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo
            {
                FileName = exeDir,
                Arguments = args,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = procStartInfo;
                process.Start();
                process.BeginOutputReadLine();

                StreamReader myStreamReader = process.StandardError;
                Console.WriteLine(myStreamReader.ReadLine());
                process.WaitForExit();
            }

            return true;
        }

        private void Unpack()
        {
            /// Load the titlekey from the form
            string titleKey = TitleKey.Text;

            /// If there's a titlekey there then use it!
            if (!String.IsNullOrEmpty(titleKey) && titleKey.Length == 32)
                titleKey = "--titlekey=" + titleKey;
            else
                titleKey = "";

            /// Single (All) or Secret Bulk (NCA Only currently...)?
            if (bulkUnpack)
            {
                /// Decrypt each file in the selected directory!
                string[] files = Directory.GetFiles(OpenFolderDialog.SelectedPath, "*.nca", SearchOption.TopDirectoryOnly);

                foreach (string fileName in files)
                {
                    string folderName = OpenFolderDialog.SelectedPath + "\\" + Path.GetFileNameWithoutExtension(fileName);
                    Directory.CreateDirectory(folderName);

                    /// Extract the NCA files...
                    string command = String.Format(
                    "hactool.exe {0} {1} --section0dir={2}\\Section0 --section1dir={3}\\Section1 --section2dir={4}\\Section2 {5}",
                    keyFile, titleKey, folderName, folderName, folderName, fileName);

                    ExecuteCommand("cmd.exe", "/C " + command);

                    /// Extract any npdm files...
                    string npdm = String.Format("{0}\\Section0\\main.npdm", folderName);
                    if (File.Exists(npdm))
                        ExecuteCommand("cmd.exe", String.Format("/C hactool.exe {0} --intype=npdm {1} >{2}.txt", keyFile, npdm, npdm));
                }

                MessageBox.Show("Unpacked all files!", "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                bulkUnpack = false;
            }
            else
            {
                /// Validate File and Folder Names
                string fileName = Path.GetFileName(InputFile.Text);
                //MessageBox.Show(fileName);
                string folderName = Path.GetDirectoryName(InputFile.Text) + "\\" + Path.GetFileNameWithoutExtension(InputFile.Text);
                //MessageBox.Show(folderName);

                /// Create the output directory
                Directory.CreateDirectory(folderName);

                /// Check Ouput Options
                string headerOutput = "";
                if (Header.Checked)
                    headerOutput = String.Format("--header={0}\\header-{1}", folderName, fileName);

                string onlyUpdatedOutput = "";
                if (OnlyUpdated.Checked)
                    onlyUpdatedOutput = "--onlyupdated";

                string plaintextOutput = "";
                if (Plaintext.Checked)
                    plaintextOutput = String.Format("--plaintext={0}\\plaintext-{1}", folderName, fileName);

                /* hactool.exe -k prod.keys --titlekey=0123456789ABCDEF0123456789ABCDEF
                   --section0dir=program\Section0 --section1dir=program\Section1 --section2dir=program\Section2 program.nca
                */

                string command = "";
                string extension = "";
                switch (Path.GetExtension(InputFile.Text))
                {
                    case ".nsp":
                        extension = "NSP";
                        command = String.Format(
                        "hactool.exe {0} --intype=pfs0 --outdir={1} {2}",
                        keyFile, folderName, InputFile.Text);
                        //MessageBox.Show(command);

                        ExecuteCommand("cmd.exe", "/C " + command);
                        break;

                    case ".xci":
                        extension = "XCI";
                        command = String.Format(
                        "hactool.exe {0} --intype=xci --outdir={1} {2}",
                        keyFile, folderName, InputFile.Text);
                        //MessageBox.Show(command);

                        ExecuteCommand("cmd.exe", "/C " + command);
                        break;

                    default:
                        extension = "NCA";
                        command = String.Format(
                        "hactool.exe {0} {1} --section0dir={2}\\Section0 --section1dir={3}\\Section1 --section2dir={4}\\Section2 {5} {6} {7} {8}",
                        keyFile, titleKey, folderName, folderName, folderName, plaintextOutput, headerOutput, onlyUpdatedOutput, InputFile.Text);
                        //MessageBox.Show(command);

                        ExecuteCommand("cmd.exe", "/C " + command);

                        /* Waiting for hactool.exe with the latest patches...
                         * --json={1}.txt
                         */
                        string npdm = String.Format("{0}\\Section0\\main.npdm", folderName);
                        if (File.Exists(npdm))
                        {
                            command = String.Format(
                            "hactool.exe {0} --intype=npdm {1} >{2}.txt",
                            keyFile, npdm, npdm);

                            ExecuteCommand("cmd.exe", "/C " + command);
                        }
                        break;
                }

                MessageBox.Show("Successfully Unpacked " + extension + " to...\n\n" + folderName, "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        #region ButtonHandlers
        private void OpenKeys_Click(object sender, EventArgs e)
        {
            /// Pick a keys file
            if (OpenKeysDialog.ShowDialog() == DialogResult.OK)
            {
                /// Keys file chosen
                keyFile = "-k " + Path.GetFileName(OpenKeysDialog.FileName);
            }

            /// If no Default Keys file, ask if they want it to be?
            if (!File.Exists(defaultKeysFile))
            {
                if (MessageBox.Show("Do you want to set this as your default keys?\n\n", "Set Default Keys", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    File.Copy(OpenKeysDialog.FileName, defaultKeysFile);
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                // Open Folder dialog
                if (OpenFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Populate the Textbox with the folder
                    InputFile.Text = OpenFolderDialog.SelectedPath;
                    Start.Enabled = true;
                    
                    // Output Options? No
                    Header.Visible = false;
                    OnlyUpdated.Visible = false;
                    Plaintext.Visible = false;
                }
            }
            else
            {
                /// Open File dialog
                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    /// Populate the Textbox with the file
                    InputFile.Text = OpenFileDialog.FileName;
                    //MessageBox.Show(Path.GetExtension(InputFile.Text));

                    switch(Path.GetExtension(InputFile.Text))
                    {
                        case ".xci":
                            /// Output Options? No
                            Header.Visible = false;
                            OnlyUpdated.Visible = false;
                            Plaintext.Visible = false;
                            break;

                        case ".nsp":
                            /// Output Options? No
                            Header.Visible = false;
                            OnlyUpdated.Visible = false;
                            Plaintext.Visible = false;
                            break;

                        default:
                            /// Output Options? Yes
                            Header.Visible = true;
                            OnlyUpdated.Visible = true;
                            Plaintext.Visible = true;
                            break;
                    }

                    Start.Enabled = true;
                    bulkUnpack = false;
                }
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            /// Use the keys file?
            if (!String.IsNullOrEmpty(keyFile))
            {
                /// No keys file to use!
                MessageBox.Show("No keys file to use, be sure to select your keys file!\n\n", "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            /// Background Processing!
            UnpackingLabel.Visible = true;
            hactoolProgress.Visible = true;
            hactoolProgress.Style = ProgressBarStyle.Marquee;

            Start.Enabled = false;
            backgroundHactool.RunWorkerAsync();
        }
        #endregion

        #region BackgroundHandler
        private void backgroundHactool_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            /// Unpack the files!
            Unpack();
        }

        private void backgroundHactool_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            /// Hide the Progress bar
            UnpackingLabel.Visible = false;
            hactoolProgress.Visible = false;
            Start.Enabled = true;
        }
        #endregion

        #region OutputOptions
        private void Header_Click(object sender, EventArgs e)
        {
            if (isHeader && Header.Checked)
            {
                Header.Checked = false;
                isHeader = Header.Checked;
            }
        }

        private void OnlyUpdated_Click(object sender, EventArgs e)
        {
            if (isOnlyUpdated && OnlyUpdated.Checked)
            {
                OnlyUpdated.Checked = false;
                isOnlyUpdated = OnlyUpdated.Checked;
            }
        }

        private void Plaintext_Click(object sender, EventArgs e)
        {
            if (isPlaintext && Plaintext.Checked)
            {
                Plaintext.Checked = false;
                isPlaintext = Plaintext.Checked;
            }
        }
        #endregion

        #region Variables
        private bool bulkUnpack = false;
        private bool isHeader = false;
        private bool isOnlyUpdated = false;
        private bool isPlaintext = false;
        private readonly string defaultKeysFile = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\.switch\\prod.keys";
        private readonly string switchHome = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\.switch";
        private string keyFile = "";
        #endregion
    }
}