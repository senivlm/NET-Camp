using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_08_2
{
    public class LogRecordTimeAsc : IComparer<LogRecord>
    {
        int IComparer<LogRecord>.Compare(LogRecord x, LogRecord y)
        {
            if (x.Time < y.Time)
            {
                return -1;
            }
            else if (x.Time > y.Time)
            {
                return 1;
            }
            return 0;
        }
    }

}
