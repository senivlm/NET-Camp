using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_07_1
{
    public class Storage
    {
        #region fields
        private int currentSize = 0;
        private Product[] products = new Product[0];
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
        public Storage() : this(0) { }
        public Storage(int size) => InitNewStorage(size);
        public Storage(params Product[] productsInit)
        {
            InitNewStorage(productsInit.Length);
            foreach (Product product in productsInit)
            {
                Add(product);
            }
        }
        #endregion

        #region indexes
        public Product this[int index]
        {
            get
            {
                if (index >= currentSize || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                return products[index];
            }
            set
            {
                //You can only write to an initialized area or to the first non-initialized
                if ((index < 0) || (index >= products.Length) || (index > currentSize))
                {
                    throw new IndexOutOfRangeException();
                }
                products[index] = value;
                if (index == currentSize)
                {
                    currentSize++;
                }
            }
        }
        #endregion

        #region methods
        public void InitNewStorage(int size)
        {
            this.products = new Product[size];
            this.currentSize = 0;
        }

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
                    fileName = ExtInputAction?.Invoke()??"";
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


        public bool Add(Product prod)
        {
            if (currentSize == products.Length)
            {
                ExtDisplayAction?.Invoke("Storage is full");
                return false;
            }
            products[currentSize++] = prod;
            return true;
        }

        public void ShowAll()
        {

            ExtDisplayAction?.Invoke("");
            ExtDisplayAction?.Invoke("ShowAll");

            foreach (Product prod in this.products)
            {
                ExtDisplayAction?.Invoke(prod?.ToString()??"empry");
            }
        }

        public void SetPrice(float percent)
        {
            foreach (Product prod in this.products)
            {
                prod.SetPrice(percent);
            }
        }
        #endregion


    }
}
