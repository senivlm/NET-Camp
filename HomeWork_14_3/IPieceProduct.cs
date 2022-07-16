using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_14_3
{
    //штучний товар
    public interface IPieceProduct : IProduct
    {
        public int QuantityInBox { get; }
    }
}
