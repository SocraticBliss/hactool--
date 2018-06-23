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

            // Does the switch home directory exist?
            if (!Directory.Exists(switchHome))
                Directory.CreateDirectory(switchHome);

            // hactool.exe Depedency Check!
            string[] files = { "hactool.exe", "libmbedcrypto.dll", "libmbedtls.dll", "libmbedx509.dll" };
            foreach (string fileName in files)
            {
                if (!File.Exists(fileName))
                {
                    MessageBox.Show(fileName + " must be in your directory to continue!", "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Environment.Exit(-1);
                }
            }

            // Check for a default Keys File...
            DefaultKeyFileCheck();
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

        private void DefaultKeyFileCheck()
        {
            // Do we have the default key file?
            if (!File.Exists(defaultKeysFile))
            {
                // No default key file? Lets import one!
                if (OpenKeysDialog.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("You need a keys file to use this program", "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Do you want to make it your default?
                if (MessageBox.Show("Do you want to make this your default prod.keys file?", "prod.keys import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    File.Copy(OpenKeysDialog.FileName, defaultKeysFile);
            }
        }

        private void NCAUnpack()
        {
            // Load the titlekey from the form
            string titleKey = TitleKey.Text;

            // If there's a titlekey there then use it!
            if (!String.IsNullOrEmpty(titleKey) && titleKey.Length == 32)
                titleKey = "--titlekey=\"" + titleKey + "\"";
            else
                titleKey = "";

            // Single or Secret Bulk?
            if (bulkUnpack)
            {
                // Decrypt each file in the selected directory!
                string[] files = Directory.GetFiles(OpenFolderDialog.SelectedPath, "*.nca", SearchOption.TopDirectoryOnly);

                foreach (string fileName in files)
                {
                    string folderName = Path.GetFileNameWithoutExtension(fileName);
                    Directory.CreateDirectory(folderName);

                    // Extract the NCA files...
                    string command = String.Format(
                    "hactool.exe {0} {1} --section0dir={2}\\Section0 --section1dir={3}\\Section1 --section2dir={4}\\Section2 {5}",
                    keyFile, titleKey, folderName, folderName, folderName, fileName);

                    ExecuteCommand("cmd.exe", "/C " + command);

                    // Extract any npdm files...
                    string npdm = folderName + "\\Section0\\main.npdm";
                    if (File.Exists(npdm))
                        ExecuteCommand("cmd.exe", String.Format("/C hactool.exe {0} --intype=npdm {1} >{2}.txt", keyFile, npdm, npdm));
                }
            }
            else
            {
                // Validate File and Folder Names
                string fileName = Path.GetFileName(InputFile.Text);
                string folderName = Path.GetFileNameWithoutExtension(InputFile.Text);

                // Get Ouput Options
                string plaintextOutput = "";
                if (Plaintext.Checked)
                    plaintextOutput = "--plaintext=plaintext-" + fileName;

                string headerOutput = "";
                if (Header.Checked)
                    headerOutput = "--header=header-" + fileName;

                string onlyUpdatedOutput = "";
                if (OnlyUpdated.Checked)
                    onlyUpdatedOutput = "--onlyupdated";

                // Create the output directory
                Directory.CreateDirectory(folderName);

                // Example command...
                // hactool.exe -k prod.keys --titlekey="0123456789ABCDEF0123456789ABCDEF" --section0dir=program\Code --section1dir=program\Data --section2dir=program\Logo program.nca 
                string command = String.Format(
                "hactool.exe {0} {1} --section0dir={2}\\Code --section1dir={3}\\Data --section2dir={4}\\Logo {5} {6} {7} {8}",
                keyFile, titleKey, folderName, folderName, folderName, plaintextOutput, headerOutput, onlyUpdatedOutput, InputFile.Text);

                // Uncomment for testing hactool command text...
                //MessageBox.Show(command);
                ExecuteCommand("cmd.exe", "/C " + command);

                /* Gotta build a hactool.exe with the latest patches... sigh
                 * --json={1}.txt
                 */
                string npdm = folderName + "\\Code\\main.npdm";
                if (File.Exists(npdm))
                {
                    command = String.Format(
                    "hactool.exe {0} --intype=npdm {1} >{2}.txt",
                    keyFile, npdm, npdm);

                    ExecuteCommand("cmd.exe", "/C " + command);
                }

                MessageBox.Show("Successfully Unpacked NCA to...\n\n" + Directory.GetCurrentDirectory() + "\\" + folderName, "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        #region ButtonHandlers
        private void OpenKeys_Click(object sender, EventArgs e)
        {
            // Pick an alternate keys file
            if (OpenKeysDialog.ShowDialog() != DialogResult.OK)
            {
                if (!File.Exists(defaultKeysFile))
                {
                    MessageBox.Show("You need a keys file to use this program", "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                } 
            }

            // Alternate keys file chosen
            keyFile = "-k " + Path.GetFileName(OpenKeysDialog.FileName);
        }

        private void Open_Click(object sender, EventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                // Open NCA Folder dialog
                if (OpenFolderDialog.ShowDialog() != DialogResult.OK)
                    return;

                // Populate the Textbox with the folder
                InputFile.Text = OpenFolderDialog.SelectedPath;
                bulkUnpack = true;
            }
            else
            {
                // Open NCA File dialog
                if (OpenFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                
                // Populate the Textbox with the file
                InputFile.Text = OpenFileDialog.FileName;
                bulkUnpack = false;
            }
        }

        private void Start_Click(object sender, EventArgs e)
        {
            // Use the alternate keys file?
            if (!String.IsNullOrEmpty(keyFile))
            {
                // No keys file to use!
                MessageBox.Show("No keys file to use!\n\n" + switchHome, "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            MessageBox.Show("Unpacked all NCA files!", "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            bulkUnpack = false;
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

        #region variables
        private bool bulkUnpack = false;
        private bool isHeader = false;
        private bool isOnlyUpdated = false;
        private bool isPlaintext = false;
        private string keyFile = "";
        private readonly string defaultKeysFile = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\.switch\\prod.keys";
        private readonly string switchHome = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\.switch";
        #endregion
    }
}