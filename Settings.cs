using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace armipsSimpleGui
{
    class Settings
    {
        public const string SETTINGS_FILENAME = "settings.xml";
        public static uint fileRAM = 0;
        public static List<String> uselibs = new List<String>();
        public static string preASM = "";
        public static string postASM = "";
        public static bool useASMasROOT = true;
        public static bool showSuccessMessageBox = true;

        public static void saveString(string text, string path) {
            File.WriteAllText(path, text);
        }
        
        public static string loadString(string path)
        {
            return File.ReadAllText(path);
        }

        public static void clearSettings()
        {
            uselibs.Clear();
            fileRAM = 0;
        }

        public static void ReadFile(string path)
        {
            clearSettings();
            XmlDocument doc = new XmlDocument();
            doc.Load(path);
            XmlNodeList parentNode = doc.GetElementsByTagName("settings");
            //Console.WriteLine("\nReading Nodes...\n");
           // Console.WriteLine(doc.InnerXml);
            XmlNode settings = parentNode.Item(0);
            foreach (XmlNode child in settings.ChildNodes)
            {
                if (child.Name.Equals("lib"))
                {
                    uselibs.Add(child.InnerText);
                }
                else if (child.Name.Equals("fileRAM"))
                {
                    uint.TryParse(child.InnerText, 
                        System.Globalization.NumberStyles.HexNumber, 
                        null, out fileRAM);
                }
                else if (child.Name.Equals("asmDirIsRoot"))
                {
                    bool.TryParse(child.InnerText, out useASMasROOT);
                }
                else if (child.Name.Equals("showSuccessBox"))
                {
                    bool.TryParse(child.InnerText, out showSuccessMessageBox);
                }
            }

        }

        public static void SaveSettings()
        {
            List<String> list = new List<String>();
            list.Add("<fileRAM>" + fileRAM.ToString("X") + "</fileRAM>");
            list.Add("<asmDirIsRoot>" + useASMasROOT.ToString() + "</asmDirIsRoot>");
            list.Add("<showSuccessBox>" + showSuccessMessageBox.ToString() + "</showSuccessBox>");

            foreach (String s in uselibs)
            {
                list.Add("<lib>" + s + "</lib>");
            }
            WriteFileDirectly(main.getActiveProfilePath() + SETTINGS_FILENAME, list);
            saveString(preASM, main.getActiveProfilePath() + "preLibASM.txt");
            saveString(postASM, main.getActiveProfilePath() + "postLibASM.txt");
        }

        public static void WriteFileDirectly(string path, List<String> list)
        {
            XDocument doc = new XDocument();

            XElement xe = new XElement
            (
                "settings",
                list
                .Select
                (
                    x =>
                    XElement.Parse(x)
                )
            );
            doc.Add(xe);
            doc.Save(path);
        }

        public static void loadPrePostASM() {
            if (File.Exists(main.getActiveProfilePath() + "preLibASM.txt"))
            {
                preASM = loadString(main.getActiveProfilePath() + "preLibASM.txt");
            }
            else
            {
                preASM = "// This code will run BEFORE the libraries have loaded." + Environment.NewLine + 
                Environment.NewLine;
                saveString(preASM, main.getActiveProfilePath() + "preLibASM.txt");
            }

            if (File.Exists(main.getActiveProfilePath() + "postLibASM.txt"))
            {
                postASM = loadString(main.getActiveProfilePath() + "postLibASM.txt");
            }
            else
            {
                postASM = "// This code will run AFTER the libraries have loaded.";
                saveString(postASM, main.getActiveProfilePath() + "postLibASM.txt");
            }
        }
    }
}
