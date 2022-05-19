using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CAMP_HomeWork_s1_01
{
    public class Buy
    {
        //Fields
        private Product? productItem;
        private int volume;

        //Properties
        public Product? ProductItem
        {
            get => this.productItem;
            set
            {
                this.productItem = value;
                CalculateBuying();
            }
        }
        public int Volume
        {
            get => this.volume;
            set
            {
                this.volume = (value >= 0) ? value: 0;
                CalculateBuying();
            }
        }
        public float Amount { get; private set; }
        public float Weight { get; private set; }

        //Constructors
        public Buy() { }
        public Buy(Product product) : this() => this.ProductItem = product;
        public Buy(Product product, int volume) : this(product) => this.Volume = volume;


        //Methods
        private void CalculateBuying()
        {
            this.Amount = (this.ProductItem != null) ? this.ProductItem.Price * this.Volume : 0;
            this.Weight = (this.ProductItem != null) ? this.ProductItem.Weight * this.Volume : 0;
        }
    }
}
