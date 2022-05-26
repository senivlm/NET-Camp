using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _InternetShop
{
    public class Dairy_products : Product
    {
        //Properties
        public ushort ExpirationDays { set; get; }

        //Constructors
        //Свідомо не робив конструктор без параметрів, 3oоб захиститісь від створення об'ектів без ініціализованих обов`язкових полів\властивостей. Відкритий до дискусії стосовно цього.
        public Dairy_products(string name, ushort expirationDays) : base(name) => this.ExpirationDays = expirationDays;
        public Dairy_products(string name, ushort expirationDays, float price, float weight) : base(name, price, weight) => this.ExpirationDays = expirationDays;

        //Methods
        public override string? ToString()
        {
            return String.Format($"Dairy_products name={this.Name} expirationDays={this.ExpirationDays} price={this.Price} weight={this.Weight}");
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            if (obj is Dairy_products)
            {
                return base.Equals(obj)
                    && this.ExpirationDays == (obj as Dairy_products)?.ExpirationDays
                    ;
            }
            else
            {
                return false;
            }
        }

        public override void SetPrice(float percent)
        {
            base.SetPrice(percent);

            float percentExpirationDays;

            if (this.ExpirationDays < 5) percentExpirationDays = 1;
            else if (this.ExpirationDays < 10) percentExpirationDays = 2;
            else percentExpirationDays = 4;

            this.Price = this.Price * (1 + percentExpirationDays / 100);
        }


    }
}
