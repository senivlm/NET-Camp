using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CAMP_HomeWork_s1_01
{
    public static class Check
    {//краще на вході тільки покупку. Групу товарів окремого покупця
        public static void ShowAbout(Product product)
        {
            Console.WriteLine($"Product {product.Name} Price {product.Price} TotalWeight {product.Weight}");
        }


        public static void ShowAbout(Buy buy)
        {
            Console.WriteLine($"Product {buy.ProductItem?.Name} Volume {buy.Volume} TotalAmount {buy.TotalAmount} TotalWeight {buy.TotalWeight}");
        }

    }
}
