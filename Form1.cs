using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;

namespace armipsSimpleGui
{
    public partial class main : Form
    {

        private string additionalParamters = "";

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
            }
        }

        public void setAdditionalParameters(string str)
        {
            additionalParamters = str;
        }

        public string getAdditionalParameters()
        {
            return additionalParamters;
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
        
        private void assemble_Click(object sender, EventArgs e)
        {

            if (!File.Exists(Directory.GetCurrentDirectory() + "/data/armips.exe"))
            {
                MessageBox.Show("Could not find armips.exe! Aborting operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!File.Exists(Directory.GetCurrentDirectory() + "/data/n64crc.exe"))
            {
                MessageBox.Show("Could not find n64crc.exe! Aborting operation!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (File.Exists(asmTextBox.Text))
            {
                if (romTextBox.Text.Length > 0)
                {
                    if (File.Exists(romTextBox.Text))
                    {
                        string createText = "SAG_FILEPATH equ \"" + romTextBox.Text + "\"" + Environment.NewLine;
                        createText += "SAG_FILEPOS equ 0x" + Settings.fileRAM.ToString("X") + Environment.NewLine;

                        createText += ".Open SAG_FILEPATH, SAG_FILEPOS" + Environment.NewLine;
                        if (Settings.preASM.Length > 0)
                            createText += Settings.preASM + Environment.NewLine;
                        foreach (string lib in Settings.uselibs)
                        {
                            createText += ".include \""+Directory.GetCurrentDirectory()+"\\data\\libs\\" + 
                                lib + "\\" + lib + ".asm\"" + Environment.NewLine;
                        }
                        if(Settings.postASM.Length > 0)
                            createText += Settings.postASM + Environment.NewLine;
                        createText += ".include \"" + asmTextBox.Text + "\"" + Environment.NewLine;
                        createText += ".Close" + Environment.NewLine;
                        //Console.WriteLine(createText);
                        File.WriteAllText("temp.asm", createText);

                        Process p = new Process();
                        p.StartInfo.FileName = "\"" + Directory.GetCurrentDirectory()+"\\data\\armips.exe\"";
                        if (Settings.useASMasROOT)
                        {
                            p.StartInfo.Arguments +=
                                "-root \"" + asmTextBox.Text.Substring(0, asmTextBox.Text.LastIndexOf("\\") + 1).Replace("\\","/") + "\" ";
                        }
                        p.StartInfo.Arguments += "\"" + Directory.GetCurrentDirectory() + "\\temp.asm\"";
                        p.StartInfo.Arguments += additionalParamters + " ";
                        Console.WriteLine(p.StartInfo.Arguments);
                        p.StartInfo.RedirectStandardOutput = true;
                        p.StartInfo.UseShellExecute = false;
                        p.Start();
                        string q = "";
                        while (!p.HasExited)
                        {
                            q += p.StandardOutput.ReadToEnd();
                           // Console.WriteLine(q.Length);
                        }

                        if (p.ExitCode != 0)
                        {
                            Form2 form2 = new Form2(q);
                            form2.ShowDialog();
                        }
                        else
                        {
                            Process checksum = new Process();
                            checksum.StartInfo.FileName = Directory.GetCurrentDirectory() + "/data/n64crc.exe";
                            checksum.StartInfo.Arguments = "\"" + romTextBox.Text + "\"";
                            checksum.Start();
                            checksum.WaitForExit();
                            MessageBox.Show("File assembled successfully to ROM.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        File.Delete("temp.asm");
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string about = "Armips created by Kingcom" + Environment.NewLine;
            about += "GUI created by davideesk" + Environment.NewLine + Environment.NewLine;
            about += "Yes, I shamelessly copied Tarek's CajeASM GUI design." + Environment.NewLine;
            about += "I liked using CajeASM before it died. RIP 2015-2016";
            MessageBox.Show(about, "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(this);
            form3.ShowDialog();
           // Console.WriteLine(additionalParamters);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.ShowDialog();
        }

        private void main_Load(object sender, EventArgs e)
        {
            Settings.loadPrePostASM();
            if (!File.Exists(Settings.PATH))
            {
                List<String> list = new List<String>();
                list.Add("<fileRAM>00000000</fileRAM>");
                list.Add("<asmDirIsRoot>True</asmDirIsRoot>");
                list.Add("<lib>sm64mlib</lib>");
                Settings.WriteFileDirectly(Settings.PATH, list);
            }
            else
            {
                Settings.ReadFile(Settings.PATH);
            }
        }
    }
}
