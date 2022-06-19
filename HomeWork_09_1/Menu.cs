using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_09_1
{
    public class Menu : IEnumerable
    {
        private List<Dish> _dishes;
        public int Length => _dishes.Count;

        public Menu()
        {
            _dishes = new List<Dish>();
        }
        public Menu(List<Dish> dishes) : this()
        {
            _dishes.AddRange(dishes);
        }
        public Dish this[int index]
        {
            //
            get => _dishes[index]; //I don't make an exception. We are satisfied with the standard IndexOutOfRangeException
        }
        public void Add(Dish dish)
        {
            _dishes.Add(dish);
        }
        public override string? ToString()
        {
            StringBuilder sb = new();
            foreach (Dish dish in _dishes)
            {
                if (sb.Length != 0)
                {
                    sb.AppendLine();
                }
                sb.Append(dish.ToString());
            }
            return sb.ToString();
        }

        public IEnumerator GetEnumerator()
        {
            return _dishes.GetEnumerator();
        }
    }
}

