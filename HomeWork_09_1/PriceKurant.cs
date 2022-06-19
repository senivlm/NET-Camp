using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_09_1
{
    public class PriceKurant
    {
        private Dictionary<string, double> _productPrice;
        public PriceKurant()
        {
            _productPrice = new();
        }
        public double this[string key]
        {
            get => _productPrice[key];
        }

        public bool TryGetProductPrice(string productName, out double price)
        {
            if (!_productPrice.TryGetValue(productName, out double result))
            {
                price = default;
                return false;
            }
            price = result;
            return true;
        }
        public void Add(string name, double value)
        {
            _productPrice[name] = value;
        }

        public override string? ToString()
        {
            StringBuilder sb = new();
            foreach(KeyValuePair<string, double> pair in _productPrice)
            {
                sb.AppendLine($"{pair.Key} -  {String.Format("{0:0.###}", pair.Value)}");
            }
            return sb.ToString();
        }
    }
}

