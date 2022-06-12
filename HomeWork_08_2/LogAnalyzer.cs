using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_08_2
{
    public class LogAnalyzer
    {
        #region fields
        private readonly List<LogRecord> storage;
        #endregion

        #region delegates
        public Action<string>? ExtDisplayAction;
        #endregion
        
        #region events
        public event Action<string>? LoggerErrorAdd;
        #endregion

        #region constructors
        public LogAnalyzer() => storage = new();
        #endregion

        #region methods
        public void Add(LogRecord rec)
        {
            storage.Add(rec);
        }
        public void Load(string nameFile)
        {
            storage.Clear();
            try
            {
                using (StreamReader sr = new StreamReader(nameFile))
                {
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            string? line = sr.ReadLine();
                            if (!String.IsNullOrEmpty(line)) 
                            {
                                storage.Add(new LogRecord(line));
                            }
                        }
                        catch (ArgumentException ex)
                        {
                            LoggerErrorAdd?.Invoke(ex.Message);
                        }
                        // The rest of the exceptions are passed above.
                    }
                }
            }
            catch (Exception ex)
            {
                LoggerErrorAdd?.Invoke($"Error while working with file> {ex.Message}");
            }
        }
        public Dictionary<object, LogAnalyzer> SplitByField(Func<LogRecord, object> funcRecord)
        {
            Dictionary<object, LogAnalyzer> dictionary = new();
            foreach (LogRecord record in storage)
            {
                var key = funcRecord(record);
                if (!dictionary.ContainsKey(key))
                {
                    LogAnalyzer newLogAnalyzer = new();
                    newLogAnalyzer.ExtDisplayAction = this.ExtDisplayAction;
                    newLogAnalyzer.LoggerErrorAdd += this.LoggerErrorAdd;
                    dictionary[key] = newLogAnalyzer;
                }
                dictionary[key].Add(record);
            }
            return dictionary;
        }


        public int Count() => storage.Count;
        public Dictionary<object, int> CountConnect(Func<LogRecord, object> funcRecord)
        {
            Dictionary<object, int> dictionary = new();
            foreach (LogRecord record in storage)
            {
                var key = funcRecord(record);
                if (!dictionary.ContainsKey(key))
                {
                    dictionary[key] = 1;
                }
                else
                {
                    dictionary[key]++;
                }
            }

            //foreach(var pair in dictionary)
            //{
            //    ExtDisplayAction?.Invoke($"{pair.Key}\t{pair.Value}");
            //}

            return dictionary;
        }



        public void ShowStat(string header)
        {
            if (ExtDisplayAction == null)
            {
                return;
            }

            ExtDisplayAction?.Invoke("");
            ExtDisplayAction?.Invoke(header);

            if (storage.Count == 0)
            {
                ExtDisplayAction?.Invoke("Storage is empry");
                return;
            }
            
            //Для кожного ip вкажіть кількість відвідувань на тиждень
            ExtDisplayAction?.Invoke($"Count visit {Count()}");

            //Найбільш популярний день
            var dicConnectDay = CountConnect((LogRecord record) => record.Day);
            KeyValuePair<object, int>? maxDay = null;
            foreach(var pair in dicConnectDay)
            {
                if (maxDay == null)
                {
                    maxDay = pair;
                }
                else if (maxDay?.Value < pair.Value)
                {
                    maxDay = pair;
                }
            }
            ExtDisplayAction?.Invoke($"The most popular day is {maxDay?.Key}");

            //найбільш популярний відрізок часу довжиною в одну годину.
            storage.Sort(new LogRecordTimeAsc());
            LogRecord populaHour = new();
            int countPopular = 0;
            TimeOnly hour23 = new TimeOnly(23, 0, 0);
            for (int i = 0; i < storage.Count; i++)
            {
                TimeOnly endHour;
                int startIndex;
                int countCurrentPopular = 1;

                LogRecord currentRec = storage[i];
                if (currentRec.Time >= new TimeOnly(23,0,0))
                {
                    endHour = new TimeOnly(0, currentRec.Time.Minute, currentRec.Time.Second);
                    startIndex = 0;
                    countCurrentPopular += (storage.Count - i - 1); // Add quantity after current
                }
                else
                {
                    endHour = new TimeOnly(currentRec.Time.Hour + 1, currentRec.Time.Minute, currentRec.Time.Second);
                    startIndex = i + 1;
                }

                for (int j = startIndex; j < storage.Count; j++)
                {
                    if (storage[j].Time < endHour)
                    {
                        countCurrentPopular++;
                    }
                    else
                    {
                        break;
                    }
                }

                if (countCurrentPopular > countPopular)
                {
                    countPopular = countCurrentPopular;
                    populaHour = currentRec;
                }
            }
            ExtDisplayAction?.Invoke($"The most popular hour begin from {populaHour.Time}, count {countPopular}");
        }
        #endregion
    }
}
