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
            if (!File.Exists("hactool.exe"))
            {
                MessageBox.Show("hactool.exe must be in your directory to continue!", "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            // Does the switch home directory exist?
            if (!Directory.Exists(switchHome))
                Directory.CreateDirectory(switchHome);

            // Check for a default Keys File...
            DefaultKeyFileCheck();
        }

        private void DefaultKeyFileCheck()
        {
            // Do we have the default key file?
            if (!File.Exists(defaultKeysFile))
            {
                // No default key file? Lets import one!
                if (openKeyManager.ShowDialog() != DialogResult.OK)
                {
                    MessageBox.Show("You need a keys file to use this program", "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Do you want to make it your default?
                if (MessageBox.Show("Do you want to make this your default prod.keys file?", "prod.keys import", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    File.Copy(openKeyManager.FileName, defaultKeysFile);
            }
        }

        private void BulkUnpack()
        {
            MessageBox.Show("Hidden NCA Bulk Decryption! ¯\\_(ツ)_/¯");

            // Decrypt each file in the selected directory!
            string[] files = Directory.GetFiles(openFolderDialog.SelectedPath, "*.nca", SearchOption.TopDirectoryOnly);
            foreach (string fileName in files)
            {
                string folderName = Path.GetFileNameWithoutExtension(fileName);
                Directory.CreateDirectory(folderName);

                // Extract the NCA files...
                string command = String.Format(
                "hactool.exe {0} --section0dir={1}\\Code --section1dir={2}\\Data --section2dir={3}\\Logo {4}",
                keyFile, folderName, folderName, folderName, fileName);

                // Extract the npdm files...
                ExecuteCommand("cmd.exe", "/C " + command);

                string npdm = folderName + "\\Code\\main.npdm";
                if (File.Exists(npdm))
                    ExecuteCommand("cmd.exe", String.Format("/C hactool.exe --intype=npdm {0} >{1}.txt", npdm, npdm));
            }

            bulkUnpack = false;
            MessageBox.Show("Unpacked all NCA files!", "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void KeyManager_Click(object sender, EventArgs e)
        {
            // Pick an alternate keys file
            if (openKeyManager.ShowDialog() != DialogResult.OK)
            {
                if (File.Exists(defaultKeysFile))
                {
                    MessageBox.Show("Ok, will continue to use the default keys file", "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    return;
                }
                else
                {
                    MessageBox.Show("You need a keys file to use this program", "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                } 
            }

            // Alternate keys file chosen
            keyFile = "-k " + Path.GetFileName(openKeyManager.FileName);
        }

        private void Open_Click(object sender, EventArgs e)
        {
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            {
                // Open NCA Folder dialog
                if (openFolderDialog.ShowDialog() != DialogResult.OK)
                    return;

                // Secret Shift-Key = Hidden NCA Bulk Decryption! (>'-')> (^'-'^) <('-'<)
                bulkUnpack = true;
            }
            else
            {
                // Open NCA File dialog
                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    return;
                
                // Populate the Textbox with the file
                InputFileBox.Text = openFileDialog.FileName;
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

            if (bulkUnpack)
                BulkUnpack();

            // Load the titlekey from the form
            string titleKey = TitleKeyInput.Text;

            // If there's a titlekey there then use it!
            if (!String.IsNullOrEmpty(titleKey) && titleKey.Length == 32)
                titleKey = "--titlekey=\"" + titleKey + "\"";
            else
                titleKey = "";

            // Validate File and Folder Names
            string fileName = Path.GetFileName(InputFileBox.Text);
            string folderName = Path.GetFileNameWithoutExtension(InputFileBox.Text);

            if (!fileName.Contains(".nca"))
            {
                // Not an NCA!
                MessageBox.Show("No NCA file to unpack!\n\n" + switchHome, "¯\\_(ツ)_/¯", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

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
            // hactool.exe -k keys.dat --titlekey="0123456789ABCDEF0123456789ABCDEF" --section0dir=program\Code --section1dir=program\Data --section2dir=program\Logo program.nca 
            string command = String.Format(
            "hactool.exe {0} {1} --section0dir={2}\\Code --section1dir={3}\\Data --section2dir={4}\\Logo {5} {6} {7} {8}",
            keyFile, titleKey, folderName, folderName, folderName, plaintextOutput, headerOutput, onlyUpdatedOutput, InputFileBox.Text);

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

            MessageBox.Show("Successfully Unpacked the NCA to...\n\n" + Directory.GetCurrentDirectory() + "\\" + folderName, "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        public bool ExecuteCommand(string exeDir, string args)
        {
            try
            {
                ProcessStartInfo procStartInfo = new ProcessStartInfo();

                procStartInfo.FileName = exeDir;
                procStartInfo.Arguments = args;
                procStartInfo.RedirectStandardOutput = true;
                procStartInfo.UseShellExecute = false;
                procStartInfo.CreateNoWindow = true;

                using (Process process = new Process())
                {
                    process.StartInfo = procStartInfo;
                    process.Start();

                    string result = process.StandardOutput.ReadToEnd();
                    Console.WriteLine(result);
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("*** Error occured executing the following commands.");
                Console.WriteLine(exeDir);
                Console.WriteLine(args);
                Console.WriteLine(ex.Message);
                return false;
            }
        }


        bool isPlaintext = false;
        private void Plaintext_Click(object sender, EventArgs e)
        {
            if (isPlaintext && Plaintext.Checked)
                Plaintext.Checked = false;
                isPlaintext = Plaintext.Checked;
        }

        bool isHeader = false;
        private void Header_Click(object sender, EventArgs e)
        {
            if (isHeader && Header.Checked)
                Header.Checked = false;
                isHeader = Header.Checked;
        }

        bool isOnlyUpdated = false;
        private void OnlyUpdated_Click(object sender, EventArgs e)
        {
            if (isOnlyUpdated && OnlyUpdated.Checked)
                OnlyUpdated.Checked = false;
                isOnlyUpdated = OnlyUpdated.Checked;
        }

        private string keyFile = "";
        private bool bulkUnpack = false;
        private readonly string defaultKeysFile = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\.switch\\prod.keys";
        private readonly string switchHome = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%") + "\\.switch";
    }
}
