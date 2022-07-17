using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_14_3
{
    public class Storage<T> : IEnumerable<T> where T:Product
    {
        #region fields
        private static readonly Lazy<Storage<T>> lazy = new Lazy<Storage<T>>(() => new Storage<T>());
        private readonly string guid;    
        private readonly List<T> products;
        #endregion

        #region fields
        public string Guid => guid;
        #endregion

        #region delegates
        public static Action<string>? ExtDisplayAction;
        #endregion

        #region constructors
        private Storage()
        {
            ExtDisplayAction?.Invoke($"Конструктор Storage() {DateTime.Now.TimeOfDay}");
            guid = System.Guid.NewGuid().ToString();
            products = new();
        }
        public static Storage<T> GetInstance()
        {
            ExtDisplayAction?.Invoke($"Отримання одинака GetInstance() {DateTime.Now.TimeOfDay}");
            return lazy.Value;
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
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            ExtDisplayAction?.Invoke($"IEnumerable<T>.GetEnumerator() {DateTime.Now.TimeOfDay}");
            return products.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            ExtDisplayAction?.Invoke($"IEnumerable.GetEnumerator() {DateTime.Now.TimeOfDay}");
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
            ExtDisplayAction?.Invoke($"");
            ExtDisplayAction?.Invoke($"Storage: {Guid}");
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
        #endregion

    }
}
