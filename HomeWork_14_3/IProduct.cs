using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_14_3
{
    public interface IProduct : IComparable<IProduct>
    {
        public string Name { get; }
        public string Unit { get; } //Одиниця виміру
        public float Price { get; set; }

    }
}
 