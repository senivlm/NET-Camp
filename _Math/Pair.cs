using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Math
{
    public class Pair
    {

        public int Number { get; set; }
        public int Freq { get; set; }


        public Pair(int number, int freq)
        {
            this.Number = number;
            this.Freq = freq;
        }

        public override bool Equals(object? obj)
        {
            //if (obj == null)
            //    return false;
            if (!(obj is Pair))
                return false;
            //if (this == null)
            //    return false;

            Pair other = (Pair)obj;
            return this.Freq == other.Freq
                && this.Number == other.Number
                ;

        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string? ToString()
        {
            return $"(Number={Number} Freq={Freq})";
        }
    }
}
