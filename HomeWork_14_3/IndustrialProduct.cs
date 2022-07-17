using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_14_3
{
    public class IndustrialProduct : Product
    {
        public IndustrialProduct() : this("", "", 0) { }
        public IndustrialProduct(string name) : this(name, "", 0) { }
        public IndustrialProduct(string name, string unit, float price)
        {
            Name = name;
            Unit = unit;
            Price = price;
        }
    }
}
