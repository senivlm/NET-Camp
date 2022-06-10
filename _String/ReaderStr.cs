using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _String
{
    internal class ReaderStr
    {


        public static (string, int, double) Parse(string str)
        {
            string[] data = str.Split(" ");

            return (data[0], int.Parse(data[1]), double.Parse(data[2]));
        }

    }
}
