using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CAMP_HomeWork_s1_01
{
    public class Product
    {
        //Fields
        private float price;
        private float weight;

        //Properties
        public string Name { get; } = ""; //ініціалізую так як string може зберыгати null, по контекту задачі нам він не треба, а без цього є попередження
        public float Price { get => this.price; set { this.price = (value >= 0) ? value : 0; }}
        public float Weight { get => this.weight; set { this.weight = (value >= 0) ? value : 0; }}

        //Constructors
        //Свідомо не робив конструктор без параметрів, 3oоб захиститісь від створення об'ектів без ініціализованих обов`язкових полів\властивостей. Відкритий до дискусії стосовно цього.
        public Product(string name) => this.Name = name;
        public Product(string name, float price, float weight) : this(name)
        {
            this.Price = price;
            this.Weight = weight;
        }

    }
}
