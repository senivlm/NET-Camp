using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_11_2
{
    public class Storage<T> : IEnumerable where T : IProduct
    {
        #region fields
        private readonly List<T> products;
        #endregion

        #region delegates
        public Action<string>? ExtDisplayAction;
        public Func<string?>? ExtInputAction;
        #endregion

        #region events
        public event Action<string>? LoggerErrorAdd;
        public event Action<string>? LoggerSuccessAdd;
        #endregion

        #region constructors
        public Storage() => products = new();
        public Storage(params T[] productsInit) : this()
        {
            foreach (T product in productsInit)
            {
                Add(product);
            }
        }
        public Storage(IEnumerable<T> productsInit) : this()
        {
            foreach (T product in productsInit)
            {
                Add(product);
            }
        }
        public Storage(IEnumerable<T> productsInit, Storage<T> storageContext) : this(productsInit)
        {
            this.ExtDisplayAction = storageContext.ExtDisplayAction;
            this.ExtInputAction = storageContext.ExtInputAction;
            this.LoggerErrorAdd += storageContext.LoggerErrorAdd;
            this.LoggerSuccessAdd += storageContext.LoggerSuccessAdd;
        }
        #endregion

        #region indexes
        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= products.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                return products[index];
            }
            set
            {
                if (index < 0 || index >= products.Count)
                {
                    throw new IndexOutOfRangeException();
                }
                products[index] = value;
            }
        }
        #endregion

        #region overrided_methods
        public static Storage<T> operator -(Storage<T> a, Storage<T> b)
        {
            //Delete in A what is in B
            Dictionary<T, int> dic1 = a.ConvertToDictionaty();
            Dictionary<T, int> dic2 = b.ConvertToDictionaty();

            List<T> ListResult = new();

            foreach (KeyValuePair<T, int> pair in dic1)
            {
                int count;
                T currentProduct = pair.Key;
                if (!dic2.ContainsKey(currentProduct))
                {
                    count = pair.Value;
                }
                else
                {
                    count = pair.Value - dic2[currentProduct];
                }
                while (count > 0)
                {
                    ListResult.Add(currentProduct);
                    count--;
                }
            }

            return new Storage<T>(ListResult, a);
        }
        public static Storage<T> operator &(Storage<T> a, Storage<T> b)
        {
            //Save in A what is and in A and in B
            Dictionary<T, int> dic1 = a.ConvertToDictionaty();
            Dictionary<T, int> dic2 = b.ConvertToDictionaty();

            List<T> ListResult = new();

            foreach (KeyValuePair<T, int> pair in dic1)
            {
                int count = 0;
                T currentProduct = pair.Key;
                if (dic2.ContainsKey(currentProduct))
                {
                    count = (pair.Value > dic2[currentProduct]) ? dic2[currentProduct] : pair.Value;
                }
                while (count > 0)
                {
                    ListResult.Add(currentProduct);
                    count--;
                }
            }

            return new Storage<T>(ListResult, a);
        }
        public IEnumerator GetEnumerator()
        {
            return products.GetEnumerator();
        }
        #endregion

        #region methods
        public void Add(T prod)
        {
            products.Add(prod);
        }
        public void ShowAll(string header = "ShowAll")
        {
            ExtDisplayAction?.Invoke("");
            ExtDisplayAction?.Invoke(header);
            if (this.products.Count == 0)
            {
                ExtDisplayAction?.Invoke("Storage Empry");
            }
            else
            {
                foreach (T prod in this.products)
                {
                    ExtDisplayAction?.Invoke(prod?.ToString() ?? "Empry");
                }
            }

        }
        public Storage<T> Except(Storage<T> secondStor)
        {
            IEnumerable<T> newListForStorage = this.products.Except(secondStor.products);
            return new Storage<T>(newListForStorage, this);
        }
        public Storage<T> Intersect(Storage<T> secondStor)
        {
            IEnumerable<T> newListForStorage = this.products.Intersect(secondStor.products);
            return new Storage<T>(newListForStorage, this);
        }
        #endregion

        #region private_methods
        private Dictionary<T, int> ConvertToDictionaty()
        {
            Dictionary<T, int> dictionary = new();
            foreach (T prod in products)
            {
                if (dictionary.ContainsKey(prod))
                {
                    dictionary[prod]++;
                }
                else
                {
                    dictionary[prod] = 1;
                }
            }
            return dictionary;
        }
        #endregion

    }
}
