using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pyatnashki
{
    public static class FileHandler
    {
        private static string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Pyatnashki by whoyandog");
        private static string fileName = "\\SaveData.txt";

        public static bool CheckDirectory()
        {
            if (Directory.Exists(path))
                return true;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }

            return false;
        }

        public static async void SaveResults(string text)
        {
            if (CheckDirectory())
                using (FileStream fs = new FileStream(path + fileName, FileMode.Append))
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(text);
                    await fs.WriteAsync(buffer, 0, buffer.Length);
                }
        }

        public static async void ReadResults()
        {
            if (CheckDirectory())
                using (FileStream fs = new FileStream(path + fileName, FileMode.OpenOrCreate))
                {
                    byte[] buffer = new byte[fs.Length];
                    await fs.ReadAsync(buffer, 0, buffer.Length);

                    EventHandler.GetInstance().OnLoadResults(ConvertToResults(Encoding.UTF8.GetString(buffer)));
                }
        }
        
        public static List<DataResult> ConvertToResults(string text)
        {
            List<DataResult> dataResults = new List<DataResult>();

            string[] list = text.Split('\n');

            for (int index = 0; index < list.Length; index++)
            {
                string[] words = list[index].Split('|');
                if (words.Length <= 1) continue;

                DataResult result = new DataResult(index + 1);
                
                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i].Contains("tS"))
                    {
                        result.dateSave = ReadData(words, i);
                        continue;
                    }

                    if (words[i].Contains("tF"))
                    {
                        result.typeField = ReadData(words, i);
                        continue;
                    }

                    if (words[i].Contains("tC"))
                    {
                        result.timeComplete = ReadData(words, i);
                        continue;
                    }

                    if (words[i].Contains("c"))
                    {
                        result.count = ReadData(words, i);
                        continue;
                    }
                }

                dataResults.Add(result);
            }

            string ReadData(string[] words, int index)
            {
                index++;

                if (words.Length <= index) return "";

                if (!words[index].Contains("tS") && !words[index].Contains("tF") && !words[index].Contains("tC") && !words[index].Contains("c"))
                    return words[index];

                return "";
            }

            return dataResults;
        }

        public static string ConvertToSave(DateTime dateTime, int rows, int columns, int counter)
        {
            return $"tS | " + DateTime.Now + " | "
                  + "tF | " + rows + "x" + columns + " | "
                  + "tC | " + dateTime.ToString("HH:mm:ss") + " | " 
                  + "c | " + counter + " "
                  + "\n";
        }
    }
}
