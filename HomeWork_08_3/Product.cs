using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_08_3
{
    public class Product : IEquatable<Product>
    {
        //Fields
        private float price;
        private float weight;

        //Properties
        public string Name { get; } = ""; //так як string може зберыгати null, по контекту задачі нам він не треба.
        public float Price { get => this.price; set { this.price = (value >= 0) ? value : 0; } }
        public float Weight { get => this.weight; set { this.weight = (value >= 0) ? value : 0; } }

        //Constructors
        //Свідомо не робив конструктор без параметрів, 3oоб захиститісь від створення об'ектів без ініціализованих обов`язкових полів\властивостей. Відкритий до дискусії стосовно цього.
        public Product(string name) => this.Name = name;
        public Product(string name, float price, float weight) : this(name)
        {
            this.Price = price;
            this.Weight = weight;
        }

        //Methods
        public override string? ToString()
        {
            return String.Format($"Product name={this.Name} price={this.Price} weight={this.Weight}");
        }
        public override int GetHashCode() => (Name, Price, Weight).GetHashCode();
        public override bool Equals(object? obj)
        {
            return Equals(obj as Product);
        }
        public virtual bool Equals(Product? other) //IEquatable<Product>
        {
            if (other == null)
            {
                return false;
            }
            return this.Name.Equals(other.Name)
                && this.price == other.price
                && this.weight == other.weight
                ;
        }
        public virtual void SetPrice(float percent)
        {
            this.Price = this.Price * (1 + percent / 100);
        }

    }
}
