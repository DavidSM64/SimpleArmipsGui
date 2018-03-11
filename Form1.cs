using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace armipsSimpleGui
{
    public partial class main : Form
    {
        public List<EXECUTABLE> executables = new List<EXECUTABLE>();
        private string additionalParameters = "";
        private string longest_profile_text = "";
        private static string path_to_profiles = Directory.GetCurrentDirectory() + "/data/profiles/";
        private static string current_profile = "";
        
        public main()
        {
            if (!Directory.Exists(Directory.GetCurrentDirectory() + "\\data"))
            {
                MessageBox.Show("Error: Could not find the data folder! Aborting!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                System.Environment.Exit(1);
            }
            else
            {
                InitializeComponent();
                loadUserProfiles();
                loadAdditionalParameters();
                executables = exeConfig.getExecutables();
            }
        }

        public static string getActiveProfileName()
        {
            return current_profile;
        }

        public static string getActiveProfilePath()
        {
            return path_to_profiles + current_profile + "\\";
        }

        public void setAdditionalParameters(string str)
        {
            additionalParameters = str;
        }

        public string getAdditionalParameters()
        {
            return additionalParameters;
        }

        private void loadAdditionalParameters()
        {
            string aafile = getActiveProfilePath() + "AdditionalArguments.txt";
            if (File.Exists(aafile))
                additionalParameters = File.ReadAllText(aafile);
            else
                File.WriteAllText(aafile, "");
        }

        private void loadUserProfiles()
        {
            foreach (string profilePath in Directory.GetDirectories(path_to_profiles))
            {
                string profileName = profilePath.Remove(0, path_to_profiles.Length);
                comboSelectProfile.Items.Add(profileName);
                if (longest_profile_text.Length < profileName.Length)
                    longest_profile_text = profileName;
            }

            try
            {
                string lastProfile = File.ReadAllText(path_to_profiles + "lastProfile.txt");
                comboSelectProfile.SelectedItem = lastProfile;
            }
            catch (IOException e)
            {
                comboSelectProfile.SelectedIndex = 0;
                File.WriteAllText(path_to_profiles + "lastProfile.txt", "N64");
            }
            
            current_profile = (string)comboSelectProfile.SelectedItem;
            updateProfileComboBoxLength((string)comboSelectProfile.SelectedItem);
        }

        private void updateProfileComboBoxLength(string text)
        {
            comboSelectProfile.Width = 20 + TextRenderer.MeasureText(text, comboSelectProfile.Font).Width;
            comboSelectProfile.Location = 
                new Point((Width - 8) - comboSelectProfile.Width, comboSelectProfile.Location.Y);
            comboSelectProfile_label.Location =
                new Point((Width - 8) - comboSelectProfile.Width - comboSelectProfile_label.Width, comboSelectProfile_label.Location.Y);
        }
        
        private void comboSelectProfile_DropDown(object sender, EventArgs e)
        {
            updateProfileComboBoxLength(longest_profile_text);
        }

        private void comboSelectProfile_DropDownClosed(object sender, EventArgs e)
        {
            updateProfileComboBoxLength((string)comboSelectProfile.SelectedItem);
            File.WriteAllText(path_to_profiles + "lastProfile.txt", (string)comboSelectProfile.SelectedItem);
            current_profile = (string)comboSelectProfile.SelectedItem;
            executables = exeConfig.getExecutables();
            loadAdditionalParameters();
            changeProfileSettings();
        }

        private void changeProfileSettings()
        {
            Settings.clearSettings();
            Settings.ReadFile(getActiveProfilePath() + Settings.SETTINGS_FILENAME);
            Settings.loadPrePostASM();
        }

        private void browseROM_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "ROM files (*.z64, *.n64, *.v64, *.u64, *.bin) | *.z64; *.n64; *.v64; *.u64; *.bin";
            if (file.ShowDialog() == DialogResult.OK)
            {
                romTextBox.Text = file.FileName;
            }
        }

        private void browseASM_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "ASM files (*.asm, *.s, *.txt, *.mips) | *.asm; *.s; *.txt; *.mips";
            if (file.ShowDialog() == DialogResult.OK)
            {
                asmTextBox.Text = file.FileName;
            }
        }

        private string parse_exe_arg_variables(string arguments)
        {
            arguments = arguments.Replace("${ROM_FILE_PATH}", "\"" + romTextBox.Text + "\"");
            if (arguments.Contains("${ROM_FILE_SIZE}"))
                arguments = arguments.Replace("${ROM_FILE_SIZE}", File.ReadAllBytes(romTextBox.Text).Length.ToString());
            arguments = arguments.Replace("${ASM_FILE_PATH}", "\"" + asmTextBox.Text + "\"");
            arguments = arguments.Replace("${FILE_RAM_ADDRESS}", "0x" + Settings.fileRAM.ToString("X8"));
            arguments = arguments.Replace("${ARMIPS_ADDITIONAL_ARGUMENTS}", additionalParameters);
            return arguments;
        }

        private bool run_executable(EXECUTABLE exe)
        {
            if (exe.Enabled)
            {
                if (exe.Name.ToUpper().Equals("ARMIPS"))
                {
                    createTempFile("temp.asm", romTextBox.Text);

                    string errorOutput = "";
                    bool successfulImport = runArmipsImport("temp.asm", ref errorOutput);

                    if (!successfulImport)
                    {
                        Form2 form2 = new Form2(errorOutput);
                        form2.ShowDialog();
                        return false;
                    }
                    
                   // DeleteTempFile("temp.asm");
                }
                else
                {
                    if (exe.Path.StartsWith("./"))
                    {
                        string replace_with = Directory.GetCurrentDirectory().Replace("\\", "/") + "/";
                        //Console.WriteLine(replace_with);
                        exe.Path = exe.Path.Replace("./", replace_with);
                    }
                    //Console.WriteLine("Running... " + exe.Path);
                    Process proc = new Process();
                    proc.StartInfo.FileName = "\"" + exe.Path + "\"";
                    proc.StartInfo.Arguments = parse_exe_arg_variables(exe.Arguments);
                    proc.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                    proc.Start();
                    proc.WaitForExit();
                    proc.Close();
                }
            }
            return true;
        }

        private void assemble_Click(object sender, EventArgs e)
        {

            if (!File.Exists(Directory.GetCurrentDirectory() + "/data/armips.exe"))
            {
                MessageBox.Show("Could not find armips.exe! Aborting operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(asmTextBox.Text))
            {
                if (romTextBox.Text.Length > 0)
                {
                    if (File.Exists(romTextBox.Text))
                    {
                        bool error_occured = false;
                        foreach (EXECUTABLE exe in executables)
                        {
                            error_occured = !run_executable(exe);
                            if (error_occured)
                                break;
                        }

                        if (Settings.showSuccessMessageBox && !error_occured)
                        {
                            Focus();
                            MessageBox.Show("File assembled successfully to ROM.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }

        public void createTempFile(string tempFile, string IMPORT_TO_FILE)
        {
            string createText = "SAG_FILEPATH equ \"" + romTextBox.Text + "\"" + Environment.NewLine;
            createText += "SAG_FILEPOS equ 0x" + Settings.fileRAM.ToString("X") + Environment.NewLine;
            createText += "SAG_IMPORTPATH equ \"" + IMPORT_TO_FILE + "\"" + Environment.NewLine;

            createText += ".Open SAG_IMPORTPATH, SAG_FILEPOS" + Environment.NewLine;
            if (Settings.preASM.Length > 0)
                createText += Settings.preASM + Environment.NewLine;
            foreach (string lib in Settings.uselibs)
            {
                createText += ".include \"" + getActiveProfilePath() + "libs\\" +
                    lib + "\\" + lib + ".asm\"" + Environment.NewLine;
            }
            if (Settings.postASM.Length > 0)
                createText += Settings.postASM + Environment.NewLine;
            createText += ".include \"" + asmTextBox.Text + "\"" + Environment.NewLine;
            createText += ".Close" + Environment.NewLine;
            //Console.WriteLine(createText);
            File.WriteAllText(tempFile, createText);

        }

        public bool runArmipsImport(string tempFile, ref string errorOutput)
        {
            Process p = new Process();
            p.StartInfo.FileName = "\"" + Directory.GetCurrentDirectory() + "\\data\\armips.exe\"";
            if (Settings.useASMasROOT)
            {
                p.StartInfo.Arguments +=
                    "-root \"" + asmTextBox.Text.Substring(0, asmTextBox.Text.LastIndexOf("\\") + 1).Replace("\\", "/") + "\" ";
            }
            p.StartInfo.Arguments += "\"" + Directory.GetCurrentDirectory() + "\\" + tempFile + "\"";
            p.StartInfo.Arguments += additionalParameters + " ";
            //Console.WriteLine(p.StartInfo.Arguments);
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.UseShellExecute = false;
            p.Start();
            //string q = "";
            while (!p.HasExited)
                errorOutput += p.StandardOutput.ReadToEnd();
            bool ret_value = (p.ExitCode == 0);
            p.Close();
            return ret_value;
        }

        public void DeleteTempFile(string tempFile)
        {
            File.Delete(tempFile);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string about = "\"Armips\" was created by Kingcom" + Environment.NewLine;
            about += "\"Simple Armips GUI\" was created by davideesk" + Environment.NewLine + Environment.NewLine;
            about += "RIP CajeASM 2015-2016" + Environment.NewLine;
            MessageBox.Show(about, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new Form3(this).ShowDialog();
           // Console.WriteLine(additionalParamters);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form4(this).ShowDialog();
        }

        private void main_Load(object sender, EventArgs e)
        {
            Settings.loadPrePostASM();
            if (!File.Exists(getActiveProfilePath() + Settings.SETTINGS_FILENAME))
            {
                List<String> list = new List<String>();
                list.Add("<fileRAM>00000000</fileRAM>");
                list.Add("<asmDirIsRoot>True</asmDirIsRoot>");
                list.Add("<showSuccessBox>True</showSuccessBox>");
                Settings.WriteFileDirectly(getActiveProfilePath() + Settings.SETTINGS_FILENAME, list);
            }
            else
            {
                Settings.ReadFile(getActiveProfilePath()+Settings.SETTINGS_FILENAME);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DialogResult dlgResult = MessageBox.Show("The webpage 'https://github.com/Kingcom/armips/blob/master/Readme.md' will now open up on your default browser.", "Armips readme", MessageBoxButtons.OKCancel);
            if (dlgResult == DialogResult.OK)
                System.Diagnostics.Process.Start("https://github.com/Kingcom/armips/blob/master/Readme.md");
        }

        private void xmlTweakButton_Click(object sender, EventArgs e)
        {
            if (romTextBox.Text.Equals(""))
            {
                MessageBox.Show("You need to have a ROM file set!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (asmTextBox.Text.Equals(""))
            {
                MessageBox.Show("You need to have an ASM file set!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Form7 form7 = new Form7(romTextBox.Text, asmTextBox.Text, this);
            form7.ShowDialog();
        }
    }
}
