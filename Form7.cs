using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace armipsSimpleGui
{
    public partial class Form7 : Form
    {
        Dictionary<string, PROFILE_FOR_XML> profiles = new Dictionary<string, PROFILE_FOR_XML>();
        private const string directory_string = "./data/xmlProfiles/";
        private string currentProfile = "";
        private int currentNumTextboxes = 0;
        private string ROM_FILEPATH = null, ASM_FILEPATH = null;
        private main form1;

        public Form7(string romPath, string asmPath, main m)
        {
            InitializeComponent();
            ROM_FILEPATH = romPath;
            ASM_FILEPATH = asmPath;
            form1 = m;
            TryAddingProfilesFromDirectory(directory_string);
        }

        private void TryAddingProfilesFromDirectory(string directory)
        {
            DirectoryInfo d = new DirectoryInfo(directory);
            FileInfo[] Files = d.GetFiles("*.json");
            foreach (FileInfo file in Files)
                TryAddProfile(file.Name);
        }
        
        private void TryAddProfile(string filename)
        {
            PROFILE_FOR_XML xml = LoadProfile(filename);
            if (xml == null)
            {
                Console.WriteLine("Profile " + filename + " is null!");
                return;
            }
            if (xml.IsValid)
            {
                listBox_profiles.Items.Add(xml.Profile_Name);
                profiles.Add(xml.Profile_Name, xml);
            }
        }

        private void listBox_profiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            make_xml_patch_button.Enabled = false;
            if (listBox_profiles.SelectedIndex > -1)
            {
                PROFILE_FOR_XML xml = null;
                string profile_name = listBox_profiles.Items[listBox_profiles.SelectedIndex].ToString();
                if (profiles.TryGetValue(profile_name, out xml))
                {
                    createTextboxes(xml);
                    currentProfile = profile_name;
                    make_xml_patch_button.Enabled = true;
                }
            }
        }

        private void createTextboxes(PROFILE_FOR_XML xmlProfile)
        {
            List<string> names = new List<string>();
            List<int> textbox_count = new List<int>();
            List<string[]> textbox_defaultStrings = new List<string[]>();
            List<int[]> textbox_heights = new List<int[]>();
            xmlProfile.GetTextBoxData(ref names, ref textbox_count, ref textbox_defaultStrings, ref textbox_heights);
            panel_addBoxes.Controls.Clear();
            currentNumTextboxes = 0;
            int y_offset = 0;
            for (int i = 0; i < textbox_count.Count; i++)
            {
                Label boxLabel = new Label();
                boxLabel.Text = names[i];
                boxLabel.Location = new Point(0, y_offset + 2);
                boxLabel.Size = new Size(panel_addBoxes.Width, 16);
                y_offset += 16;
                boxLabel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                for (int j = 0; j < textbox_count[i]; j++)
                {
                    TextBox box = new TextBox();
                    box.ScrollBars = ScrollBars.Both;
                    box.Location = new Point(0, y_offset);
                    int num_rows_index = Math.Min(j, textbox_heights[i].Length - 1);
                    if (textbox_heights[i][num_rows_index] > 20)
                        box.Multiline = true;
                    if (j < textbox_defaultStrings[i].Length)
                        box.Text = textbox_defaultStrings[i][j];
                    box.Size = new Size(panel_addBoxes.Width, textbox_heights[i][num_rows_index]);
                    box.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                    panel_addBoxes.Controls.Add(box);
                    y_offset += box.Height;
                    currentNumTextboxes++;
                }
                panel_addBoxes.Controls.Add(boxLabel);
            }
        }

        private void make_xml_patch_button_Click(object sender, EventArgs e)
        {
            PROFILE_FOR_XML xml = null;
            if (profiles.TryGetValue(currentProfile, out xml))
            {
                DATA_IMPORT[] imported_data = ImportToXML.getImportedData(ROM_FILEPATH, ASM_FILEPATH, form1);
                if (imported_data != null)
                {
                    string[] textboxes = new string[currentNumTextboxes];
                    int currentTextboxIndex = 0;
                    foreach (Control control in panel_addBoxes.Controls)
                        if (control is TextBox)
                            textboxes[currentTextboxIndex++] = control.Text;
                    if (xml.BuildXMLFile(imported_data, textboxes)) {
                        MessageBox.Show("XML patch created!");
                        Hide();
                    }
                }
                else
                {
                    MessageBox.Show("XML patch could not be made");
                    Hide();
                }
            }

            /* 
                I've been having some OutOfMemory errors when I spam the create XML Patch button.
                Telling the Garabage Collector to clean up seems to fix the issue.
            */
            GC.Collect(); 
        }

        private PROFILE_FOR_XML LoadProfile(string profileFileName)
        {
            string fp = directory_string + profileFileName;

            if (File.Exists(fp))
                return new PROFILE_FOR_XML(File.ReadAllText(fp));

            return null;
        }


        private class PROFILE_FOR_XML_TREENODE
        {
            private bool isValid = false;
            public bool IsValid { get { return isValid; } }
            private bool isComment = false;
            public bool IsComment { get { return isComment; } }
            private string name = "";
            public string Name { get { return name; } }
            private PROFILE_FOR_XML_TREENODE[] children;
            public PROFILE_FOR_XML_TREENODE[] Children { get { return children; } }
            private bool hasChildren = false;
            public bool HasChildren { get { return hasChildren; } }
            private string value_str = "";
            public string Value { get { return value_str; } }
            private bool repeatForAllEntries = false;
            public bool RepeatForAllEntries { get { return repeatForAllEntries; } }

            public PROFILE_FOR_XML_TREENODE() {}

            public PROFILE_FOR_XML_TREENODE(string node_name, JArray arr)
            {
                name = node_name;
                children = new PROFILE_FOR_XML_TREENODE[arr.Count];
                value_str = "This is an array, stupid!";
                if (arr.Count > 0)
                    hasChildren = true;
                int currentChildIndex = 0;
                foreach (JObject node in arr.Children())
                {
                    if (node["Comment"] != null)
                    {
                        //Console.WriteLine("Comment: " + node["Comment"].ToString());
                        children[currentChildIndex] = new PROFILE_FOR_XML_TREENODE();
                        children[currentChildIndex].isComment = true;
                        children[currentChildIndex].isValid = true;
                        children[currentChildIndex].value_str = node["Comment"].ToString();
                        isValid = true;
                    }
                    else
                    {
                        if (node["Name"] != null && node["Value"] != null)
                        {
                            if (node["Value"].Type == JTokenType.Array)
                            {
                                children[currentChildIndex] = new PROFILE_FOR_XML_TREENODE(node["Name"].ToString(),(JArray)node["Value"]);
                            }
                            else
                            {
                                children[currentChildIndex] = new PROFILE_FOR_XML_TREENODE();
                                children[currentChildIndex].name = node["Name"].ToString();
                                children[currentChildIndex].value_str = node["Value"].ToString();
                            }

                            if (node["RepeatForAllEntries"] != null)
                            {
                                if (node["RepeatForAllEntries"].Type == JTokenType.Boolean)
                                    children[currentChildIndex].repeatForAllEntries = (bool)node["RepeatForAllEntries"];
                                else
                                    children[currentChildIndex].repeatForAllEntries = bool.Parse(node["RepeatForAllEntries"].ToString());
                                //Console.WriteLine("RepeatForAllEntries set to " + children[currentChildIndex].repeatForAllEntries + " for node: " + children[currentChildIndex].Name);
                            }

                            isValid = true;
                        }

                    }
                    currentChildIndex++;
                }
            }

            public string ToString(int indent, int repeat_amount)
            {

                string str = "";

                if (name != "")
                    str += "<" + name + ">";

                if (hasChildren)
                {
                    foreach (PROFILE_FOR_XML_TREENODE node in children)
                    {
                        if (!node.RepeatForAllEntries)
                            str += node.ToString(indent + 1, repeat_amount);
                        else
                        {
                            //Console.WriteLine("Repeating node '" + node.Name + "' " + repeat_amount + " times");
                            for(int i = 0; i < repeat_amount; i++)
                                str += node.ToString(indent + 1, repeat_amount);
                        }
                    }
                }
                else
                {
                    if (!isComment)
                        str += value_str;
                    else
                        str += "<!--" + value_str + "-->";
                }

                if (name != "")
                    str += "</" + name + ">";

                return str;
            }

            public string ToString(int repeat_amount)
            {
                return "<?xml version=\"1.0\"?>" + ToString(-1, repeat_amount);
            }

            public void PrintXMLNode()
            {
                Console.WriteLine(ToString());
            }
        }

        private enum format_source {
            ADDRESS, BYTES, TEXTBOX
        }
        
        private class PROFILE_FOR_XML_FORMAT
        {
            private format_source[] sources;
            public format_source[] Sources { get { return sources; } }
            private bool isValid = false;
            public bool IsValid { get { return isValid; } }
            private string source_format = "";
            private string byte_separator = "";
            private string entry_separator = "";
            private string address_prefix = "";
            private string address_postfix = "";
            private string byte_prefix = "";
            private string byte_postfix = "";
            private bool consume_all_entries = false;
            private int[] textbox_height = new int[] { };
            public int[] Textbox_Heights { get { return textbox_height; } }
            private string[] textbox_defaultStrings = new string[] { };
            public string[] Textbox_DefaultStrings { get { return textbox_defaultStrings; } }
            private int num_textboxes = 0;
            public int Num_Textboxes { get { return num_textboxes; } }

            public PROFILE_FOR_XML_FORMAT(JProperty format)
            {
                if (format.Value["ByteSeparator"] != null) byte_separator = format.Value["ByteSeparator"].ToString();
                if (format.Value["EntrySeparator"] != null) entry_separator = format.Value["EntrySeparator"].ToString();
                if (format.Value["AddressPrefix"] != null) address_prefix = format.Value["AddressPrefix"].ToString();
                if (format.Value["AddressPostfix"] != null) address_postfix = format.Value["AddressPostfix"].ToString();
                if (format.Value["BytePrefix"] != null) byte_prefix = format.Value["BytePrefix"].ToString();
                if (format.Value["BytePostfix"] != null) byte_postfix = format.Value["BytePostfix"].ToString();
                if (format.Value["ConsumeAllEntries"] != null)
                {
                    if (format.Value["ConsumeAllEntries"].Type == JTokenType.Boolean)
                        consume_all_entries = (bool)format.Value["ConsumeAllEntries"];
                    else
                        consume_all_entries = bool.Parse(format.Value["ConsumeAllEntries"].ToString());
                }
                if (format.Value["TextboxHeights"] != null)
                {
                    if (format.Value["TextboxHeights"].Type == JTokenType.Integer)
                    {
                        textbox_height = new int[] { (int)format.Value["TextboxHeights"] };
                    }
                    else if (format.Value["TextboxHeights"].Type == JTokenType.Array)
                    {
                        JArray tbrows = (JArray)format.Value["TextboxHeights"];
                        int num_children_tbrows = tbrows.Count;
                        textbox_height = new int[num_children_tbrows];
                        for (int i = 0; i < num_children_tbrows; i++)
                        {
                            if (tbrows[i].Type == JTokenType.Integer)
                                textbox_height[i] = (int)tbrows[i];
                            else
                                textbox_height[i] = int.Parse(tbrows[i].ToString());
                        }
                    }
                    else
                    {
                        textbox_height = new int[] { int.Parse(format.Value["TextboxHeights"].ToString()) };
                    }
                }


                if (format.Value["TextboxDefaultTexts"] != null)
                {
                    if (format.Value["TextboxDefaultTexts"].Type == JTokenType.Array)
                    {
                        JArray tbrows = (JArray)format.Value["TextboxDefaultTexts"];
                        int num_children_tbrows = tbrows.Count;
                        textbox_defaultStrings = new string[num_children_tbrows];
                        for (int i = 0; i < num_children_tbrows; i++)
                        {
                            textbox_defaultStrings[i] = tbrows[i].ToString();
                        }
                    }
                    else
                    {
                        textbox_defaultStrings = new string[] { format.Value["TextboxDefaultTexts"].ToString() };
                    }
                }

                if (format.Value["Source"] != null)
                {
                    source_format = format.Value["Source"].ToString();
                    List<int> positions = new List<int>();
                    sources = new format_source[CountStringOccurrences(source_format, "${", ref positions)];
                    //Console.WriteLine(source_format);
                    string new_source_format = "";
                    int localAdd = 0;
                    int prev_pos = 0;
                    foreach (int pos in positions)
                    {
                        new_source_format += source_format.Substring(prev_pos, pos - prev_pos) + "{" + localAdd + "}";
                        string source_name = source_format.Substring(pos + 2, source_format.IndexOf('}', pos + 2) - (pos + 2)).ToUpper();
                        if (source_name.Equals("TEXTBOX"))
                            num_textboxes++;

                        bool found_source = false;
                        foreach (format_source source in Enum.GetValues(typeof(format_source)))
                        {

                            if (source_name.Equals(source.ToString()))
                            {
                                sources[localAdd++] = source;
                                found_source = true;
                                break;
                            }
                        }

                        if(!found_source)
                            throw new Exception("INVALID LABEL: ${" + source_name + "}");
                        
                        prev_pos = pos + source_name.Length + 3;
                    }
                    source_format = new_source_format;
                    //Console.WriteLine(source_format);
                    isValid = true;
                }
            }

            private int CountStringOccurrences(string text, string pattern, ref List<int> positions)
            {
                int count = 0;
                int i = 0;
                while ((i = text.IndexOf(pattern, i)) != -1)
                {
                    positions.Add(i);
                    i += pattern.Length;
                    count++;
                }
                return count;
            }

            private string byteArrayToString(byte[] arr)
            {
                string str = "";
                for(int i = 0; i < arr.Length; i++)
                {
                    str += byte_prefix + arr[i].ToString("X2");
                    if(i < arr.Length - 1)
                        str += byte_postfix + byte_separator;
                }
                return str;
            }

            private string getAddressString(uint value)
            {
                return address_prefix + value.ToString("X") + address_postfix;
            }

            public string buildFormattedString(DATA_IMPORT[] importedData, ref int importedAddressIndex, ref int importedBytesIndex,
                string[] textboxes, ref int textboxesIndex, int indent_length)
            {

                string str = "";
                int repeat_amount = 0;

                // I think this is the first time I've ever used a do-while loop in my life.
                do {
                    string substr = source_format;
                    for (int i = 0; i < sources.Length; i++)
                    {
                        switch (sources[i])
                        {
                            case format_source.ADDRESS:
                                if (importedData.Length > 0)
                                {
                                    substr = substr.Replace("{" + i + "}", getAddressString(importedData[importedAddressIndex].Address));
                                }
                                else
                                    substr = "";
                                importedAddressIndex++;
                                break;
                            case format_source.BYTES:
                                if (importedData.Length > 0)
                                {
                                    substr = substr.Replace("{" + i + "}", byteArrayToString(importedData[importedBytesIndex].Data));
                                }
                                else
                                    substr = "";
                                importedBytesIndex++;
                                break;
                            case format_source.TEXTBOX:
                                if (textboxes.Length > 0)
                                {
                                    if (textboxesIndex < textboxes.Length)
                                        substr = substr.Replace("{" + i + "}", textboxes[textboxesIndex]);
                                }
                                else
                                    substr = "";
                                textboxesIndex++;
                                break;
                        }
                    }

                    string indent_str = "";
                    if (repeat_amount++ > 0)
                    {
                        indent_str = (entry_separator == "\n" ? Environment.NewLine : entry_separator);
                        if(indent_str.EndsWith(Environment.NewLine))
                            for (int i = 0; i < indent_length; i++)
                                indent_str += " ";
                    }

                    str += indent_str + substr;
                } while (consume_all_entries && importedAddressIndex < importedData.Length && importedBytesIndex < importedData.Length);
                return str;
            }
        }

        private class PROFILE_FOR_XML
        {
            public bool IsValid { get { return isValid; } }
            private bool isValid = false;
            private string profile_name;
            public string Profile_Name { get { return profile_name; } }
            private Dictionary<string, PROFILE_FOR_XML_FORMAT> formats = new Dictionary<string, PROFILE_FOR_XML_FORMAT>();
            PROFILE_FOR_XML_TREENODE xml_root;

            public PROFILE_FOR_XML(string json)
            {
                JObject o = JObject.Parse(json);

                if (o["PROFILE_NAME"] != null)
                {
                    profile_name = o["PROFILE_NAME"].ToString();
                    if (o["FORMAT"] != null && o["XML_TREE"] != null)
                    {
                        foreach (JProperty obj in o["FORMAT"].Children())
                        {
                            formats.Add(obj.Name, new PROFILE_FOR_XML_FORMAT(obj));
                        }
                        xml_root = new PROFILE_FOR_XML_TREENODE("", (JArray)o["XML_TREE"]);
                        isValid = true;
                    }
                }
            }

            public void GetTextBoxData(ref List<string> names, ref List<int> textbox_count, ref List<string[]> textbox_defaultStrings, 
                ref List<int[]> textbox_heights)
            {
                if (isValid)
                {
                    foreach (KeyValuePair<string, PROFILE_FOR_XML_FORMAT> entry in formats)
                    {
                        if (entry.Value.Num_Textboxes > 0)
                        {
                            names.Add(entry.Key);
                            textbox_count.Add(entry.Value.Num_Textboxes);
                            textbox_heights.Add(entry.Value.Textbox_Heights);
                            textbox_defaultStrings.Add(entry.Value.Textbox_DefaultStrings);
                        }
                    }
                }
            }

            private string ReplaceFirst(string text, string search, string replace)
            {
                int pos = text.IndexOf(search);
                if (pos < 0)
                {
                    return text;
                }
                return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
            }

            public bool BuildXMLFile(DATA_IMPORT[] importedData, string[] textboxes) {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "XML File (*.xml) | *.xml | All files (*) | *";
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string xml_template = xml_root.ToString(importedData.Length);
                    int importedAddressIndex = 0, importedBytesIndex = 0, textboxesIndex = 0;
                    if (isValid)
                    {
                        //Console.WriteLine(xml_template);
                        foreach (KeyValuePair<string, PROFILE_FOR_XML_FORMAT> entry in formats)
                        {
                            do
                            {
                                int format_pos = xml_template.IndexOf("$FORMAT{" + entry.Key + "}") - 1;
                                int indent_length = 0;

                                while (format_pos - (indent_length) >= 0 &&
                                    xml_template[format_pos - (indent_length)] == ' ')
                                    indent_length++;

                                xml_template = ReplaceFirst(xml_template, "$FORMAT{" + entry.Key + "}",
                                    entry.Value.buildFormattedString(importedData, ref importedAddressIndex, ref importedBytesIndex, textboxes, ref textboxesIndex, indent_length));
                            } while (xml_template.IndexOf("$FORMAT{" + entry.Key + "}") != -1);
                        }
                        //Console.WriteLine(xml_template);
                        File.WriteAllText(saveDialog.FileName, xml_template);
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
