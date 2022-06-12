using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_08_2
{
    public class LogRecord
    {
        #region properties
        public string IPAdress { get; }
        public TimeOnly Time { get; }
        public DayOfWeek Day { get; }
        #endregion

        #region constructors
        public LogRecord() : this("0.0.0.0", new TimeOnly(0, 0, 0), (DayOfWeek)0) { }
        public LogRecord(string ipAdress, TimeOnly time, DayOfWeek day)
        {
            this.IPAdress = ipAdress;
            this.Time = time;
            this.Day = day;
        }
        public LogRecord(string stringLog)
        {
            string[] partsLog = stringLog.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (partsLog.Length != 3)
            {
                throw new ArgumentException($"Incorrect number of fields> {stringLog}");
            }

            StringBuilder sb = new();

            //IP
            string ipAdress = partsLog[0];

            //Time
            TimeOnly time;
            if (!TimeOnly.TryParse(partsLog[1], out time))
            {
                sb.Append("time,");
            }

            //DayOfWeek
            DayOfWeek day;
            if(!DayOfWeek.TryParse(partsLog[2], out day))
            {
                sb.Append("day of week,");
            }

            if (sb.Length > 0)
            {
                sb.Insert(0, "Incorrect ");
                throw new ArgumentException($"{sb.ToString()}> {stringLog}");
            }

            this.IPAdress = ipAdress;
            this.Time = time;
            this.Day = day;

        }
        #endregion

        #region indexers
        public object this[string nameField]
        {
            get
            {
                if (nameField.Equals("IPAdress"))
                {
                    return this.IPAdress;
                }
                else if (nameField.Equals("Time"))
                {
                    return this.Time;
                }
                else if (nameField.Equals("Day"))
                {
                    return this.Day;
                }
                else
                {
                    throw new ArgumentException($"Invalid argument {nameField}");
                }
            }
        }
        #endregion






    }
}
