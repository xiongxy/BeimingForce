using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BeimingForce.ToolsKit
{
    public static class FolderHelper
    {
        public static bool IsExistFolder(string path)
        {
            return Directory.Exists(path);
        }
        public static string CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            return path;
        }

        public static bool CreateFile(string path, string content)
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(content);
            sw.Close();
            return true;
        }
        public static bool CreateFile(string filePathName, byte[] bytes)
        {
            try
            {
                if (!File.Exists(filePathName))
                {
                    File.WriteAllBytes(filePathName, bytes);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static string LodaFile(string path)
        {
            string strData = "";
            try
            {
                using (System.IO.StreamReader sr = new System.IO.StreamReader(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        strData += line + Environment.NewLine;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            return strData;
        }
    }
}