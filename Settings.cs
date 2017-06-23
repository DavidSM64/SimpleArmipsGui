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
        public const string GUI_VERSION = "1.0";
        public const string PATH = "data\\settings.xml";
        public static uint fileRAM = 0;
        public static List<String> uselibs = new List<String>();
        public static string preASM = "";
        public static string postASM = "";

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
            }

        }

        public static void SaveSettings()
        {
            List<String> list = new List<String>();
            list.Add("<fileRAM>" + fileRAM.ToString("X") + "</fileRAM>");

            foreach (String s in uselibs)
            {
                list.Add("<lib>" + s + "</lib>");
            }
            WriteFileDirectly(PATH, list);
            saveString(preASM, Directory.GetCurrentDirectory() + "\\data\\preLibASM.txt");
            saveString(postASM, Directory.GetCurrentDirectory() + "\\data\\postLibASM.txt");
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
            if (File.Exists(Directory.GetCurrentDirectory() + "\\data\\preLibASM.txt"))
            {
                preASM = loadString(Directory.GetCurrentDirectory() + "\\data\\preLibASM.txt");
            }
            else
            {
                preASM = "// This code will run BEFORE the libraries have loaded." + Environment.NewLine + 
                Environment.NewLine + 
                "// Set armips to N64 mode" + Environment.NewLine + 
                ".n64" + Environment.NewLine;
                saveString(preASM, Directory.GetCurrentDirectory() + "\\data\\preLibASM.txt");
            }

            if (File.Exists(Directory.GetCurrentDirectory() + "\\data\\postLibASM.txt"))
            {
                postASM = loadString(Directory.GetCurrentDirectory() + "\\data\\postLibASM.txt");
            }
            else
            {
                postASM = "// This code will run AFTER the libraries have loaded.";
                saveString(postASM, Directory.GetCurrentDirectory() + "\\data\\postLibASM.txt");
            }
        }
    }
}
