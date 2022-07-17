using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_14_3
{
    public class IndustrialProductFactory : IProductFactory
    {
        public IndustrialProductFactory()
        {
        }

        public Product ReadFromString(string str)
        {
            return new IndustrialProduct(str);
        }
    }
}
