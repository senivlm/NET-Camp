using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_09_1
{
    public class Order
    {
        private Dictionary<Dish, int> _dishes;
        public IEnumerable<Dish> Keys => _dishes.Keys;
        public Order()
        {
            _dishes = new();
        }
        public int this[Dish key] { get => _dishes[key]; }

        public void Add(Dish dish, int n)
        {
            if (_dishes.ContainsKey(dish))
            {
                _dishes[dish] += n;
            }
            else
            {
                _dishes[dish] = n;
            }
        }

        public override string? ToString()
        {
            StringBuilder sb = new();
            foreach(KeyValuePair<Dish, int> pair in _dishes)
            {
                sb.AppendLine($"{pair.Key.Name} - {pair.Value}");

            }
            return sb.ToString();
        }
    }
}
