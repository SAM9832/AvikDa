using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace appTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string inputFilePath = ConfigurationManager.AppSettings.Get("InputFilePath");
                string outputFilePath = ConfigurationManager.AppSettings.Get("OutputFilePath");
                string checkFilePath = ConfigurationManager.AppSettings.Get("CheckingFolderPath");
                string text = System.IO.File.ReadAllText(inputFilePath);
                string[] fileEntries = Directory.GetFiles(checkFilePath);
                string[] fileName = text.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string status = "";
                if (fileName.Length != 0)
                {
                    if (File.Exists(outputFilePath))
                    {
                        File.Delete(outputFilePath);
                    }
                }
                foreach (string fileName2 in fileName)
                {
                    if (fileEntries.Contains(ConfigurationManager.AppSettings.Get("CheckingFolderPath") + fileName2))
                    {
                        status += ConfigurationManager.AppSettings.Get("CheckingFolderPath") + fileName2 + "  _____________ Exist\n";
                    }
                    else
                    {
                        status += ConfigurationManager.AppSettings.Get("CheckingFolderPath") +  fileName2 + "  _____________ Not Exist\n";
                    }
                }
                writeFile(status);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static bool writeFile(string fileName)
        {
            try
            {
                string outputFilePath = ConfigurationManager.AppSettings.Get("OutputFilePath");
                using (FileStream fs = File.Create(outputFilePath))
                {  
                    Byte[] title = new UTF8Encoding(true).GetBytes(fileName);
                    fs.Write(title, 0, title.Length);
                    //byte[] author = new UTF8Encoding(true).GetBytes("Mahesh Chand");
                    //fs.Write(author, 0, author.Length);
                }
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
