using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_CAMP_HomeWork_s1_02_01
{
    public class Storage
    {
        //Fields
        private uint currentSize = 0;
        private Product[] products;

        //Constructors
        //Свідомо не робив конструктор без параметрів, 3oоб захиститісь від створення об'ектів без ініціализованих обов`язкових полів\властивостей. Відкритий до дискусії стосовно цього.
        public Storage(uint size) => InitNewStorage(size);
        public Storage(params Product[] productsInit)
        {
            InitNewStorage((uint)productsInit.Length);
            foreach (Product product in productsInit)
            {
                Add(product);
            }
        }

        //Methods
        public Product this[int index]
        { 
            get => products[index];
            set => products[index] = value;
        }

        public void InitNewStorage(uint size)
        {
            this.products = new Product[size];
            this.currentSize = 0;
        }

        public void Add(Product prod)
        {
            if (this.products == null)
            {
                Console.WriteLine("Input size of Storage");
                string? sizeStr = Console.ReadLine();

                uint size;
                if (!UInt32.TryParse(sizeStr, out size))
                {
                    Console.WriteLine("Storage is not initialized");
                    return;
                }

                InitNewStorage(size);
            }

            if (currentSize == products.Length)
            {
                Console.WriteLine("Storage is full");
                return;
            }

            products[currentSize] = prod;
            currentSize++;

        }

        public void ShowMeat()
        {
            Console.WriteLine();
            Console.WriteLine("ShowMeat");

            foreach (Product prod in this.products)
            {
                if (prod is Meat)
                {
                    Console.WriteLine(prod);
                }
            }
        }

        public void ShowAll()
        {

            Console.WriteLine();
            Console.WriteLine("ShowAll");

            foreach (Product prod in this.products)
            {
                Console.WriteLine(prod);
            }
        }

        public void SetPrice(float percent)
        {
            foreach (Product prod in this.products)
            {
                prod.SetPrice(percent);
            }
        }


    }
}
