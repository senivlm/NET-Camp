using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_14_3
{
    public abstract class Product
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public float Price { get; set; }

        public override string? ToString()
        {
            return $"Type: {this.GetType().Name}  Name:{Name}";
        }
    }
}
