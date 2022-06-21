﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_07_1
{
    public class Logger
    {

        #region fields
        private string fileName;
        #endregion

        #region delegates
        public Action<string>? ExtDisplayAction;
        #endregion
        
        #region constructors
        public Logger() => fileName = "";
        public Logger(string fileNameNew) => this.fileName = fileNameNew;
        #endregion

        public void Init(string fileNameNew)
        {
            if (!this.fileName.Equals(""))
            {
                ExtDisplayAction?.Invoke($"Logger already initialized in '{this.fileName}'");
                return;
            }
            this.fileName = fileNameNew;
        }
        public void Add(string message)
        {
            if (fileName.Equals(""))
            {
                ExtDisplayAction?.Invoke("Logger not initialized");
                return;

            }
            try 
            {
                using (StreamWriter sw = new StreamWriter(fileName, true))
                {
                    sw.WriteLine($"{DateTime.Now.ToString("MM/dd/yyyy HH:mm")}> {message}");
                }
            }
            catch (Exception ex)
            {
                ExtDisplayAction?.Invoke($"Error: {ex.Message}");
            }
        }

        public void ShowLog(DateTime date)
        {
            if (fileName.Equals(""))
            {
                ExtDisplayAction?.Invoke("Logger not initialized");
                return;

            }
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
                {//Уникайте continue
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        string[] arrLine = line.Split(">", StringSplitOptions.TrimEntries);
                        if (arrLine.Length < 2)
                        {
                            ExtDisplayAction?.Invoke($"Broken Log record> {line}");
                            continue;
                        }
                        DateTime dateLog;
                        if (!DateTime.TryParse(arrLine[0], out dateLog))
                        {
                            ExtDisplayAction?.Invoke("Broken Log file");
                            continue;
                        }

                        if (dateLog >= date)
                        {
                            ExtDisplayAction?.Invoke(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ExtDisplayAction?.Invoke($"Error: {ex.Message}");
            }

        }
    }
}
