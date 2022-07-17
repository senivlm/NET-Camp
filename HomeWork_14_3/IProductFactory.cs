using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_14_3
{
    public interface IProductFactory
    {
        Product ReadFromString(string str);
    }
}
