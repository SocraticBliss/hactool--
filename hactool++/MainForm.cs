using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using static System.Environment;

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
                    MessageBox.Show($@"{fileName} must be in your current directory to continue!", @"¯\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }

            /// Default Keys Check!
            if (!File.Exists(defaultKeysFile))
            {
                /// If the switch home directory doesn't exist, create it!
                Directory.CreateDirectory(switchHome);

                /// Import Keys?
                if (MessageBox.Show("Do you want to import your keys?", "Import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (OpenKeysDialog.ShowDialog() == DialogResult.OK)
                        File.Copy(OpenKeysDialog.FileName, defaultKeysFile);
                    else
                        MessageBox.Show("Be sure to import a keys file before starting...", "No default keys file", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }
        }

        private bool RunHactool(string args)
        {
            ProcessStartInfo procStartInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $@" /C {args}",
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
                string error = myStreamReader.ReadLine();

                if (error != null)
                {
                    //MessageBox.Show(error);

                    switch (error)
                    {
                        case "hactool (c) SciresM.":
                            break;

                        /// Error: section 0 is corrupted!
                        case "Error: section 0 is corrupted!":
                            if (procStartInfo.Arguments.Contains("titlekey="))
                                MessageBox.Show($@"{error}{NewLine}{NewLine}Hint: Try another titlekey?", @"hactool.exe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                MessageBox.Show($@"{error}{NewLine}{NewLine}Hint: Enter a titlekey?", @"hactool.exe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        /// Error: section 1 is corrupted!
                        case "Error: section 1 is corrupted!":
                            if (procStartInfo.Arguments.Contains("titlekey="))
                                MessageBox.Show($@"{error}{NewLine}{NewLine}Hint: Try another titlekey?", @"hactool.exe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                                MessageBox.Show($@"{error}{NewLine}{NewLine}Hint: Enter a titlekey?", @"hactool.exe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show($@"{error}", @"hactool.exe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                process.WaitForExit();
            }

            return true;
        }

        private void Unpack()
        {
            string args = "";
            string extension = "";
            string npdm = "";

            /// Load the titlekey from the form
            string titleKey = TitleKey.Text;

            /// If there's a titlekey there then use it!
            if (!String.IsNullOrEmpty(titleKey) && titleKey.Length == 32)
                titleKey = $@"--titlekey={titleKey}";
            else
                titleKey = "";

            /// Single (All extensions) or Secret Bulk (NCA Only currently...)?
            if (bulkUnpack)
            {
                /// NCA Files!
                extension = "NCA";
                string[] files = Directory.GetFiles(OpenFolderDialog.SelectedPath, "*.nca", SearchOption.TopDirectoryOnly);
                foreach (string fileName in files)
                {
                    string folderName = Path.Combine(OpenFolderDialog.SelectedPath, Path.GetFileNameWithoutExtension(fileName));
                    //MessageBox.Show(folderName);
                    Directory.CreateDirectory(folderName);

                    /// Extract the NCA files...
                    args = $@"hactool.exe {keyFile} {titleKey} --section0dir={folderName}\Section0 --section1dir={folderName}\Section1 --section2dir={folderName}\Section2 {fileName}";
                    //MessageBox.Show(args);

                    RunHactool($@"{args}");

                    /// Extract any npdm files...
                    npdm = $@"{folderName}\Section0\main.npdm";
                    if (File.Exists(npdm))
                    {
                        args = $@"hactool.exe {keyFile} {titleKey} --intype=npdm {npdm} >{npdm}.txt";
                        //MessageBox.Show(args);

                        RunHactool($@"{args}");
                    }
                }

                MessageBox.Show($@"Successfully unpacked all {extension} files in...{NewLine}{NewLine}{OpenFolderDialog.SelectedPath}", "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                bulkUnpack = false;
            }
            else
            {
                /// Validate File and Folder Names
                string fileName = Path.GetFileName(InputFile.Text);
                //MessageBox.Show(fileName);
                string folderName = Path.Combine(Path.GetDirectoryName(InputFile.Text), Path.GetFileNameWithoutExtension(InputFile.Text));
                //MessageBox.Show(folderName);

                /// Create the output directory
                Directory.CreateDirectory(folderName);

                /// Check Ouput Options
                string headerOutput = "";
                if (Header.Checked)
                    headerOutput = $@"--header={folderName}\header-{fileName}";

                string onlyUpdatedOutput = "";
                if (OnlyUpdated.Checked)
                    onlyUpdatedOutput = "--onlyupdated";

                string plaintextOutput = "";
                if (Plaintext.Checked)
                    plaintextOutput = $@"--plaintext={folderName}\plaintext-{fileName}";

                /* hactool.exe -k prod.keys --titlekey=0123456789ABCDEF0123456789ABCDEF
                   --section0dir=program\Section0 --section1dir=program\Section1 --section2dir=program\Section2 program.nca
                */

                switch (Path.GetExtension(InputFile.Text))
                {
                    case ".nsp":
                        extension = "NSP";
                        args = $@"hactool.exe {keyFile} --intype=pfs0 --outdir={folderName} {InputFile.Text}";
                        //MessageBox.Show(args);

                        RunHactool($@"{args}");
                        break;

                    case ".xci":
                        extension = "XCI";
                        args = $@"hactool.exe {keyFile} --intype=xci --outdir={folderName} {InputFile.Text}";
                        //MessageBox.Show(args);

                        RunHactool($@"{args}");
                        break;

                    case ".nca":
                    default:
                        extension = "NCA";
                        args = $@"hactool.exe {keyFile} {titleKey} --section0dir={folderName}\Section0 --section1dir={folderName}\Section1 --section2dir={folderName}\Section2 {plaintextOutput} {headerOutput} {onlyUpdatedOutput} {InputFile.Text}";
                        //MessageBox.Show(args);

                        RunHactool($@"{args}");
                        
                        /* Waiting for hactool.exe with the latest patches...
                         * --json={npdm}.txt*/
                        
                        /// Extract any npdm files...
                        npdm = $@"{folderName}\Section0\main.npdm";
                        if (File.Exists(npdm))
                        {
                            args = $@"hactool.exe {keyFile} {titleKey} --intype=npdm {npdm} >{npdm}.txt";
                            //MessageBox.Show(args);

                            RunHactool($@"{args}");
                        }
                        break;
                }

                MessageBox.Show($@"Successfully Unpacked {extension} to...{NewLine}{NewLine}{folderName}", "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        #region ButtonHandlers
        private void OpenKeys_Click(object sender, EventArgs e)
        {
            /// Pick a keys file
            if (OpenKeysDialog.ShowDialog() == DialogResult.OK)
            {
                /// Keys file chosen
                keyFile = $@"-k {Path.GetFileName(OpenKeysDialog.FileName)}";
            }

            /// If no Default Keys file, ask if they want it to be?
            if (!File.Exists(defaultKeysFile))
            {
                if (MessageBox.Show("Do you want to set this as your default keys file?", "Set Default Keys", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
                    bulkUnpack = true;

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
                    /// Populate the Textbox with the file and set as current directory
                    InputFile.Text = OpenFileDialog.FileName;
                    OpenFileDialog.InitialDirectory = OpenFileDialog.FileName;
                    //MessageBox.Show(Path.GetExtension(InputFile.Text));

                    switch (Path.GetExtension(InputFile.Text))
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

                        case ".nca":
                        default:
                            /// Output Options? Yes
                            Header.Visible = true;
                            OnlyUpdated.Visible = true;
                            Plaintext.Visible = true;
                            break;
                    }

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
                MessageBox.Show("No keys file to use, be sure to select your keys file!", @"¯\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        private readonly string defaultKeysFile = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + @"\.switch\prod.keys";
        private readonly string switchHome = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + @"\.switch";
        private string keyFile = "";
        #endregion
    }
}