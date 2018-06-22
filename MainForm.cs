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
        }

        private void Open_Click(object sender, EventArgs e)
        {
            // Secret Shift-Key = Hidden NCA Bulk Decryption! (>'-')> (^'-'^) <('-'<)
            if ((ModifierKeys & Keys.Shift) == Keys.Shift)
            { 
                openFolderDialog.ShowDialog();

                MessageBox.Show("Hidden NCA Bulk Decryption! ¯\\_(ツ)_/¯");

                // Decrypt each file in the selected directory!
                string[] files = Directory.GetFiles(openFolderDialog.SelectedPath, "*.nca", SearchOption.TopDirectoryOnly);
                foreach (string fileName in files)
                {
                    string folderName = Path.GetFileNameWithoutExtension(fileName);
                    Directory.CreateDirectory(folderName);

                    string command = String.Format(
                    "hactool.exe --section0dir={0}\\Code --section1dir={1}\\Data --section2dir={2}\\Logo {3}",
                    folderName, folderName, folderName, fileName);

                    ExecuteCommand("cmd.exe", "/C " + command);

                    string npdm = folderName + "\\Code\\main.npdm";
                    if (File.Exists(npdm))
                        ExecuteCommand("cmd.exe", String.Format("/C hactool.exe --intype=npdm {0} >{1}.txt", npdm, npdm));
                }

                MessageBox.Show("Done!", "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                openFileDialog.ShowDialog();

                InputFileBox.Text = openFileDialog.FileName;

                if (InputFileBox.Text.Contains(".nca"))
                { 
                    OutputOptionsLabel.Show();
                    Plaintext.Show();
                    Header.Show();
                    OnlyUpdated.Show();
                }
            }
        }

        private void KeyManager_Click(object sender, System.EventArgs e) => openKeyManager.ShowDialog();

        private void Start_Click(object sender, System.EventArgs e)
        {
            string keyFile = Path.GetFileName(openKeyManager.FileName);

            if (!String.IsNullOrEmpty(keyFile))
                keyFile = "-k " + keyFile;
            else
                keyFile = "";

            string titleKey = TitleKeyInput.Text;

            if (!String.IsNullOrEmpty(titleKey))
                titleKey = "--titlekey=\"" + titleKey + "\"";
            else
                titleKey = "";

            string fileLocation = InputFileBox.Text;
            string fileName = Path.GetFileName(InputFileBox.Text);
            string plaintextOutput = fileName;
            string headerOutput = fileName;
            string onlyUpdatedOutput = "";

            if (Plaintext.Checked)
                plaintextOutput = "--plaintext=plaintext-" + plaintextOutput;
            else
                plaintextOutput = "";

            if (Header.Checked)
                headerOutput = "--header=header-" + headerOutput;
            else
                headerOutput = "";

            if (OnlyUpdated.Checked)
                onlyUpdatedOutput = "--onlyupdated";

            string folderName = Path.GetFileNameWithoutExtension(InputFileBox.Text);
            Directory.CreateDirectory(folderName);

            // hactool.exe -k keys.dat --titlekey="0123456789ABCDEF0123456789ABCDEF" --section0dir=program\Code --section1dir=program\Data --section2dir=program\Logo program.nca 
            string command = String.Format(
            "hactool.exe {0} {1} --section0dir={2}\\Code --section1dir={3}\\Data --section2dir={4}\\Logo {5} {6} {7} {8}",
            keyFile, titleKey, folderName, folderName, folderName, plaintextOutput, headerOutput, onlyUpdatedOutput, fileLocation);

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

            MessageBox.Show("Done!", "Thanks SciresM!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
    }
}
