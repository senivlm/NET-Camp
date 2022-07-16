using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_14_3
{
    //Об'ємний товар
    public  interface IVolumeProduct : IProduct
    {
        public float density { get; } //Густина
    }
}
