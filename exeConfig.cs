using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace armipsSimpleGui
{
    public class EXECUTABLE
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Arguments { get; set; }
        public bool Enabled { get; set; }
    }
    class exeConfig
    {
        private static string getJsonFilePath()
        {
            return main.getActiveProfilePath() + "exeConfig.json";
        }

        public static List<EXECUTABLE> getExecutables()
        {
            List<EXECUTABLE> exes = new List<EXECUTABLE>();

            string jsonFile = getJsonFilePath();

            if (!File.Exists(jsonFile))
            {
                EXECUTABLE exe = new EXECUTABLE();
                exe.Name = "Armips";
                exe.Enabled = true;
                exes.Add(exe);
                return exes;
            }
            
            JArray jarray = (JArray)JObject.Parse(File.ReadAllText(jsonFile))["executables"];

            foreach (JObject obj in jarray)
            {
                EXECUTABLE exe = new EXECUTABLE();
                if (obj["Name"] != null &&  obj["Path"] != null 
                    && obj["Arguments"] != null && obj["Enabled"] != null) {
                    exe.Name = obj["Name"].ToString();
                    exe.Path = obj["Path"].ToString();
                    exe.Arguments = obj["Arguments"].ToString();

                    if(obj["Enabled"].Type == JTokenType.Boolean)
                        exe.Enabled = (bool)obj["Enabled"];
                    else
                        exe.Enabled = bool.Parse(obj["Enabled"].ToString());
                }

                exes.Add(exe);
            }
            return exes;
        }

        public static void writeExecutables(List<EXECUTABLE> exes)
        {

            JObject o = new JObject(
                new JProperty ("executables", 
                    new JArray(
                        from e in exes select new JObject(
                            new JProperty("Name", e.Name),
                            new JProperty("Path", e.Path),
                            new JProperty("Arguments", e.Arguments),
                            new JProperty("Enabled", e.Enabled)
                        )
                    )
                 )
             );

            File.WriteAllText(getJsonFilePath(), o.ToString());
        }
    }
}
