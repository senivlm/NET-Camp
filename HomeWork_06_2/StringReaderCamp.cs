using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_06_2
{
    public class StringReaderCamp
    {
        #region properties
        public string FileName { get; set; } = "";
        #endregion

        #region delegates
        public Action<string>? ExtAction;
        #endregion

        #region constructors
        public StringReaderCamp() { }
        public StringReaderCamp(string fileName) : this()
        {
            FileName = fileName;
        }
        #endregion

        #region methods
        public void ShowContent()
        {
            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    while (!sr.EndOfStream)
                    {
                        string? strTmp = sr.ReadLine();
                        if (strTmp != null)
                        {
                            ExtAction?.Invoke(strTmp);
                        }
                    }
                }
            }
            catch (FileNotFoundException)
            {
                ExtAction?.Invoke($"File '{FileName}' not found");
            }
            catch (Exception e)
            {
                ExtAction?.Invoke(e.Message);
            }
        }

        public List<string> SplitеTextIntoSentences()
        {
            List<string> result = new();

            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                { 
                    string currentLine = "";
                    while (!sr.EndOfStream)
                    {
                        string? lastLineRead = sr.ReadLine();
                        if (!string.IsNullOrEmpty(lastLineRead))
                        {
                            string[] subLine = lastLineRead.Split(".", StringSplitOptions.TrimEntries);

                            //До першого додаємо currentLine
                            currentLine = (string.IsNullOrEmpty(currentLine)) ? subLine[0] : $"{currentLine} {subLine[0]}";
// Алгоритмічно є п питання!
                            if (subLine.Length > 1)
                            {
                                //Нульовий у currentLine, а останній обходимо, а ле у файл не пишемо, а запам'ятовуемо у currentLine
                                for (int i = 1; i < subLine.Length; i++)
                                {
                                    result.Add(currentLine);
                                    currentLine = subLine[i];
                                }
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(currentLine)) 
                    {
                        result.Add(currentLine);
                    }

                }
            }
            catch (FileNotFoundException)
            {
                ExtAction?.Invoke($"File '{FileName}' not found");
            }
            catch (Exception e)
            {
                ExtAction?.Invoke(e.Message);
            }

            return result;

        }

        public void SaveToFile(string nameOutputFile)
        {
            SaveToFile(nameOutputFile, SplitеTextIntoSentences());
        }
        public void SaveToFile(string nameOutputFile, List<string> sentences)
        {

            try
            {
                using (StreamWriter sw = new StreamWriter(nameOutputFile))
                {
                    foreach(string sentence in sentences)
                    {
                        sw.WriteLine($"{sentence}."); 
                    }
                }
            }
            catch (Exception e)
            {
                ExtAction?.Invoke(e.Message);
            }
        }
                            
        public static (string, string) ShortLongWords(string str)
            {
                string[] tmpStr = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                if (tmpStr.Length == 0)
                {
                    return ("", "");
                }

                string shortStr = tmpStr[0];
                string longStr = tmpStr[0];

                for(int i = 1; i < tmpStr.Length; i++)
                {
                    if (tmpStr[i].Length > longStr.Length)
                    {
                        longStr = tmpStr[i];
                    }
                    if (tmpStr[i].Length < shortStr.Length)
                    {
                        shortStr = tmpStr[i];
                    }
                }
                return (shortStr, longStr);

            }
        
        public void ShowShortLongWords()
        {
            ShowShortLongWords(SplitеTextIntoSentences());
        }
        public void ShowShortLongWords(List<string> sentences)
        {
            foreach (string sentence in sentences)
            {
                ExtAction?.Invoke($"{sentence}.{StringReaderCamp.ShortLongWords(sentence).ToString()}");
            }
        }



        #endregion

    }
}
