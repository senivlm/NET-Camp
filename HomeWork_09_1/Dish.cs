using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_09_1
{
    public class Dish
    {
        private Dictionary<string, double> _ingridients;

        public int Length => _ingridients.Count;
        public IEnumerable<string> Keys => _ingridients.Keys;
        public string Name { get; set; } = "";

        public Dish()
        {
            _ingridients = new();
        }
        public Dish(string name) : this()
        {
            Name = name;
        }
        public Dish(Dictionary<string, double> ingridients) : this()
        {
            foreach (KeyValuePair<string, double> pair in ingridients)
            {
                _ingridients[pair.Key] = pair.Value;
            }
        }
        public double this[string key]
        {
            get => _ingridients[key];
            //{
            //    if (_ingridients.TryGetValue(key, out double value))
            //    {
            //        return value;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
        }

        public void Add(string name, double value)
        {
            _ingridients[name] = value;
        }

        public override string? ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine(Name);
            foreach (KeyValuePair<string, double> pair in _ingridients)
            {
                sb.AppendLine($"{pair.Key}, {String.Format("{0:0.###}", pair.Value)}");
            }
            return sb.ToString();
        }
    }
}

