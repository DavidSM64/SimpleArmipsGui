using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace armipsSimpleGui
{
    public class DATA_IMPORT
    {
        private byte[] data;
        public byte[] Data { get { return data; } }
        private uint address;
        public uint Address { get { return address; } }

        public DATA_IMPORT(uint fileAddress, byte[] importData)
        {
            address = fileAddress;
            data = importData;
        }
    }
    
    class ImportToXML
    {
        public static DATA_IMPORT[] getImportedData(string ROM_FILEPATH, string ASM_FILEPATH, main m)
        {
            List<DATA_IMPORT> data_import = new List<DATA_IMPORT>();
            byte[][] data = new byte[2][];
            
            //Use multithreading to speed things up a bit.
            Thread t1 = new Thread(() => data[0] = getImportedData(ROM_FILEPATH, ASM_FILEPATH, 0x00, m));
            Thread t2 = new Thread(() => data[1] = getImportedData(ROM_FILEPATH, ASM_FILEPATH, 0xFF, m));
            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();
            

            if (data[0] == null || data[1] == null)
                return null;

            bool getting = false;
            uint current_address = 0;
            uint current_getting_length = 0;
            for (uint i = 0; i < data[0].Length; i++)
            {
                if (data[0][i] != 0x00 || data[1][i] != 0xFF) // Found modified byte
                {
                    if (!getting)
                    {
                        current_address = i;
                        current_getting_length = 1;
                        getting = true;
                    }
                    else
                    {
                        current_getting_length++;
                    }
                }
                else if (getting)
                {
                    byte[] import_bytes = new byte[current_getting_length];
                    Array.Copy(data[0], current_address, import_bytes, 0, current_getting_length);
                    data_import.Add(new DATA_IMPORT(current_address, import_bytes));
                    getting = false;
                }
            }
            if (getting)
            {
                byte[] import_bytes = new byte[current_getting_length];
                Array.Copy(data[0], current_address, import_bytes, 0, current_getting_length);
                data_import.Add(new DATA_IMPORT(current_address, import_bytes));
                getting = false;
            }
            

            File.Delete("xml_temp0.bin");
            File.Delete("xml_temp255.bin");

            return data_import.ToArray();
        }

        private static byte[] getImportedData(string ROM_FILEPATH, string ASM_FILEPATH, byte fill, main m)
        {
            int length = (int)new FileInfo(ROM_FILEPATH).Length;
            File.WriteAllBytes("xml_temp"+fill+".bin", Superfast.InitByteArray(fill, length));
            m.createTempFile("temp"+fill+".asm", Directory.GetCurrentDirectory()+ "\\xml_temp" + fill + ".bin");
            string errorOutput = "";
            bool successful = m.runArmipsImport("temp" + fill + ".asm", ref errorOutput);
            if (!successful)
            {
                Form2 form2 = new Form2(errorOutput);
                form2.ShowDialog();
                return null;
            }
            m.DeleteTempFile("temp" + fill + ".asm");
            return File.ReadAllBytes("xml_temp" + fill + ".bin");
        }

        public static class Superfast
        {
            [DllImport("msvcrt.dll",
                      EntryPoint = "memset",
                      CallingConvention = CallingConvention.Cdecl,
                      SetLastError = false)]
            private static extern IntPtr MemSet(IntPtr dest, int c, int count);

            //If you need super speed, calling out to M$ memset optimized method using P/invoke
            public static byte[] InitByteArray(byte fillWith, int size)
            {
                byte[] arrayBytes = new byte[size];
                GCHandle gch = GCHandle.Alloc(arrayBytes, GCHandleType.Pinned);
                MemSet(gch.AddrOfPinnedObject(), fillWith, arrayBytes.Length);
                gch.Free();
                return arrayBytes;
            }
        }
    }
}
