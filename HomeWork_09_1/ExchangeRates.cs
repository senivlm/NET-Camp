using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_09_1
{
    public class ExchangeRates
    {
        private Dictionary<string, double> _exchangeRates;
        public ExchangeRates()
        {
            _exchangeRates = new();
        }
        public KeyValuePair<string, double> this[string key]
        {
            get => new KeyValuePair<string, double> (key, _exchangeRates[key]);
        }

        public void Add(string name, double value)
        {
            _exchangeRates[name] = value;
        }

        public override string? ToString()
        {
            StringBuilder sb = new();
            foreach (KeyValuePair<string, double> pair in _exchangeRates)
            {
                sb.AppendLine($"{pair.Key} -  {String.Format("{0:0.###}", pair.Value)}");
            }
            return sb.ToString();
        }
    }
}


