using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_08_2
{
    public static class GeneratorOfLogs
    {
        public static void Create(string fileName, int n)
        {
            Random ran = new();
            StringBuilder sb = new();
            using (StreamWriter sw = new StreamWriter(fileName))
            {
                for (int i = 0; i < n; i++)
                {
                    sb.Clear();
                    //IP adress
                    sb.Append("139.18.150.");
                    sb.Append(ran.Next(0, 256).ToString());
                    sb.Append(" ");

                    //Time
                    sb.Append(String.Format("{0:d2}", ran.Next(0, 24)));
                    sb.Append(":");
                    sb.Append(String.Format("{0:d2}", ran.Next(0, 60)));
                    sb.Append(":");
                    sb.Append(String.Format("{0:d2}", ran.Next(0, 60)));
                    sb.Append(" ");

                    //day of week
                    sb.Append(((DayOfWeek)ran.Next(0, 7)).ToString());

                    sw.WriteLine(sb.ToString());
                }
            }
        }
    }
}
