using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_14_3
{
    //ваговий товар
    public interface IWeightProduct : IProduct
    {
        public float FractionSize { get; }
    }
}
