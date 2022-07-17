using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_14_3
{
    public class FoodProductFactory : IProductFactory
    {
        public FoodProductFactory()
        {
        }

        public Product ReadFromString(string str)
        {
            return new FoodProduct(str);
        }
    }
}
