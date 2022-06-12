using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math
{
    public class IntAscComparer : IComparer<int>
    {
        int IComparer<int>.Compare(int x, int y)
        {
            return (x - y);
        }
    }

    public class IntDescComparer : IComparer<int>
    {
        int IComparer<int>.Compare(int x, int y)
        {
            return (y - x);
        }
    }
}
