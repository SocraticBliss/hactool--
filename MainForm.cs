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

            // hactool.exe Depedency Check!
            string[] files = { "hactool.exe", "libmbedcrypto.dll", "libmbedtls.dll", "libmbedx509.dll" };
            foreach (string fileName in files)
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show(fileName + " must be in your current directory to continue!\n\n", "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(0);
                }
            }

            // Default Keys Check!
            if (!File.Exists(defaultKeysFile))
            {
                // If the switch home directory doesn't exist, create it!
                Directory.CreateDirectory(switchHome);

                // Import Keys?
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
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = procStartInfo;
                process.Start();

                string result = process.StandardOutput.ReadToEnd();
                Console.WriteLine(result);
            }

            return true;
        }

        private void NCAUnpack()
        {
            // Load the titlekey from the form
            string titleKey = TitleKey.Text;

            // If there's a titlekey there then use it!
            if (!String.IsNullOrEmpty(titleKey) && titleKey.Length == 32)
                titleKey = "--titlekey=" + titleKey;
            else
                titleKey = "";

            // Single or Secret Bulk?
            if (bulkUnpack)
            {
                // Decrypt each file in the selected directory!
                string[] files = Directory.GetFiles(OpenFolderDialog.SelectedPath, "*.nca", SearchOption.TopDirectoryOnly);

                foreach (string fileName in files)
                {
                    string folderName = OpenFolderDialog.SelectedPath + "\\" + Path.GetFileNameWithoutExtension(fileName);
                    Directory.CreateDirectory(folderName);

                    // Extract the NCA files...
                    string command = String.Format(
                    "hactool.exe {0} {1} --section0dir={2}\\Section0 --section1dir={3}\\Section1 --section2dir={4}\\Section2 {5}",
                    keyFile, titleKey, folderName, folderName, folderName, fileName);

                    ExecuteCommand("cmd.exe", "/C " + command);

                    // Extract any npdm files...
                    string npdm = String.Format("{0}\\Section0\\main.npdm", folderName);
                    if (File.Exists(npdm))
                        ExecuteCommand("cmd.exe", String.Format("/C hactool.exe {0} --intype=npdm {1} >{2}.txt", keyFile, npdm, npdm));
                }

                MessageBox.Show("Unpacked all NCA files!", "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                bulkUnpack = false;
            }
            else
            {
                // Validate File and Folder Names
                string fileName = Path.GetFileName(InputFile.Text);
                //MessageBox.Show(fileName);
                string folderName = Path.GetDirectoryName(InputFile.Text) + "\\" + Path.GetFileNameWithoutExtension(InputFile.Text);
                //MessageBox.Show(folderName);

                // Create the output directory
                Directory.CreateDirectory(folderName);

                // Check Ouput Options
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
                   --section0dir=program\Code --section1dir=program\Data --section2dir=program\Logo program.nca
                */
                string command = String.Format(
                "hactool.exe {0} {1} --section0dir={2}\\Code --section1dir={3}\\Data --section2dir={4}\\Logo {5} {6} {7} {8}",
                keyFile, titleKey, folderName, folderName, folderName, plaintextOutput, headerOutput, onlyUpdatedOutput, InputFile.Text);
                //MessageBox.Show(command);

                ExecuteCommand("cmd.exe", "/C " + command);

                /* Waiting for hactool.exe with the latest patches...
                 * --json={1}.txt
                 */
                string npdm = String.Format("{0}\\Code\\main.npdm", folderName);
                if (File.Exists(npdm))
                {
                    command = String.Format(
                    "hactool.exe {0} --intype=npdm {1} >{2}.txt",
                    keyFile, npdm, npdm);

                    ExecuteCommand("cmd.exe", "/C " + command);
                }

                MessageBox.Show("Successfully Unpacked NCA to...\n\n" + folderName, "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        #region ButtonHandlers
        private void OpenKeys_Click(object sender, EventArgs e)
        {
            // Pick a keys file
            if (OpenKeysDialog.ShowDialog() == DialogResult.OK)
            {
                // Keys file chosen
                keyFile = "-k " + Path.GetFileName(OpenKeysDialog.FileName);
            }

            // If no Default Keys file, ask if they want it to be?
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
                // Open NCA Folder dialog
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
                // Open NCA File dialog
                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Populate the Textbox with the file
                    InputFile.Text = OpenFileDialog.FileName;
                    bulkUnpack = false;

                    // Output Options? Yes
                    Header.Visible = true;
                    OnlyUpdated.Visible = true;
                    Plaintext.Visible = true;
                }
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            // Use the keys file?
            if (!String.IsNullOrEmpty(keyFile))
            {
                // No keys file to use!
                MessageBox.Show("No keys file to use, be sure to select your keys file!\n\n", "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Background Processing!
            hactoolProgress.Visible = true;
            hactoolProgress.Style = ProgressBarStyle.Marquee;

            Start.Enabled = false;
            backgroundHactool.RunWorkerAsync();
        }
        #endregion

        #region BackgroundHandler
        private void backgroundHactool_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            // Unpack the NCA's!
            NCAUnpack();
        }

        private void backgroundHactool_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            // Hide the Progress bar
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