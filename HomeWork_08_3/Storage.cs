using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_08_3
{
    public class Storage : IEnumerable
    {
        #region fields
        private List<Product> products = new();
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
        public Storage() { }
        public Storage(params Product[] productsInit) : this()
        {
            foreach (Product product in productsInit)
            {
                Add(product);
            }
        }
        public Storage(IEnumerable<Product> productsInit) : this()
        {
            foreach (Product product in productsInit)
            {
                Add(product);
            }
        }
        public Storage(IEnumerable<Product> productsInit, Storage storageContext) : this(productsInit)
        {
            this.ExtDisplayAction = storageContext.ExtDisplayAction;
            this.ExtInputAction = storageContext.ExtInputAction;
            this.LoggerErrorAdd += storageContext.LoggerErrorAdd;
            this.LoggerSuccessAdd += storageContext.LoggerSuccessAdd;
        }

        #endregion

        #region indexes
        public Product this[int index]
        {
            get
            {
                if (index >= products.Count || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                return products[index];
            }
            set
            {
                if ((index < 0) || (index >= products.Count))
                {
                    throw new IndexOutOfRangeException();
                }
                products[index] = value;
            }
        }
        #endregion

        #region overrided_methods
        public static Storage operator -(Storage a, Storage b)
        {
            //Delete in A what is in B
            IEnumerable<Product> newListForStorage = new List<Product>();
            newListForStorage = a.products.Except(b.products);
            return new Storage(newListForStorage, a);
        }
        public static Storage operator &(Storage a, Storage b)
        {
            //Delete in A what is in B
            IEnumerable<Product> newListForStorage = new List<Product>();
            newListForStorage = a.products.Intersect(b.products);
            return new Storage(newListForStorage, a);
        }
        public IEnumerator GetEnumerator()
        {
            return products.GetEnumerator();
        }
        #endregion

        #region methods
        public void ReadProductsFromFile(string path, string fileName)
        {
            int numberOfTries = 3;
            bool repeat;

            do
            {
                repeat = false;
                try
                {
                    if (string.IsNullOrEmpty(fileName))
                    {
                        throw new FileNotFoundException("Name file is empry");
                    }
                    using (StreamReader reader = new StreamReader(path + fileName))
                    {
                        ReadProductsFromStream(reader);
                    }
                }
                catch (FileNotFoundException) when (ExtInputAction != null && numberOfTries > 0)
                {
                    numberOfTries--;
                    repeat = true;

                    ExtDisplayAction?.Invoke("Please enter a valid filename");
                    fileName = ExtInputAction?.Invoke() ?? "";
                }
                catch (Exception ex)
                {
                    ExtDisplayAction?.Invoke(ex.Message);
                }
            } while (repeat);
        }
        public void ReadProductsFromStream(StreamReader reader)
        {
            while (!reader.EndOfStream)
            {
                try
                {
                    string curString = reader.ReadLine();

                    if (curString != null && !ReadProductsFromString(curString))
                    {
                        continue;
                    }
                }

                catch (Exception ex)
                {
                    ExtDisplayAction?.Invoke(ex.Message);
                    break;
                }
            }

        }
        public bool ReadProductsFromString(string curString)
        {
            string[] arrString = curString.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (arrString.Length != 3)
            {
                LoggerErrorAdd?.Invoke($"Incorrect number of fields> {curString}");
                return false;
            }

            StringBuilder sb = new();

            //Price
            float price;
            if (!float.TryParse(arrString[1], out price))
            {
                sb.Append("field Price,");
            }

            //Weight
            float weight;
            if (!float.TryParse(arrString[2], out weight))
            {
                sb.Append("field Weight,");
            }

            if (sb.Length > 0)
            {
                sb.Insert(0, "Incorrect ");
                LoggerErrorAdd?.Invoke($"{sb.ToString()}> {curString}");
                return false;
            }

            //Name
            string name = curString[0].ToString().ToUpper() + curString.Substring(1);

            Add(new Product(name, price, weight));
            LoggerSuccessAdd?.Invoke($"add> {curString}");

            return true;
        }
        public void RepeatReadFromString(string str)
        {
            Console.WriteLine("Line with error:");
            Console.WriteLine(str);
            Console.WriteLine("Input correct line:");

            string? strInput = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(strInput))
            {
                ReadProductsFromString(strInput);
            }

        }
        public void Add(Product prod)
        {
            products.Add(prod);
        }
        public void ShowAll(string header = "ShowAll")
        {
            ExtDisplayAction?.Invoke("");
            ExtDisplayAction?.Invoke(header);
            if (this.products.Count == 0 )
            {
                ExtDisplayAction?.Invoke("Storage Empry");
            }
            else
            {
                foreach (Product prod in this.products)
                {
                    ExtDisplayAction?.Invoke(prod?.ToString() ?? "Empry");
                }
            }

        }
        public Dictionary<Product, int> ConvertToDictionaty()
        {
            Dictionary<Product, int> dictionary = new();

            foreach(Product prod in products)
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
        public Storage Except(Storage secondStor)
        {
            Dictionary<Product, int> dic1 = this.ConvertToDictionaty();
            Dictionary<Product, int> dic2 = secondStor.ConvertToDictionaty();

            List<Product> ListResult = new();

            foreach (KeyValuePair<Product, int> pair in dic1)
            {
                int count;
                Product currentProduct = pair.Key;
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

            return new Storage(ListResult, this);

        }

        public Storage Intersect(Storage secondStor)
        {
            Dictionary<Product, int> dic1 = this.ConvertToDictionaty();
            Dictionary<Product, int> dic2 = secondStor.ConvertToDictionaty();

            List<Product> ListResult = new();

            foreach (KeyValuePair<Product, int> pair in dic1)
            {
                int count = 0;
                Product currentProduct = pair.Key;
                if (dic2.ContainsKey(currentProduct))
                {
                    count = (pair.Value > dic2[currentProduct])? dic2[currentProduct]: pair.Value;
                }
                while (count > 0)
                {
                    ListResult.Add(currentProduct);
                    count--;
                }
            }

            return new Storage(ListResult, this);

        }
        #endregion


    }
}
