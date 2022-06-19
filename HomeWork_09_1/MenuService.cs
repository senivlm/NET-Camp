using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_09_1
{
    public static class MenuService
    {
        static private readonly int numberTry = 3;

        #region delegates
        static public Action<string>? ExtDisplayAction;
        static public Func<string?>? ExtInputAction;
        #endregion

        #region events
        static public event Action<string>? LoggerErrorAdd;
        #endregion

        static public bool TryGetMenuTotalSum(Menu menu, PriceKurant priceKurant, out double menuTotalSum)
        {
            menuTotalSum = default;
            for (int i = 0; i < menu.Length; i++)
            {
                if (!TryGetDishPrice(menu[i], priceKurant, out double sumPrice))
                {
                    sumPrice = default;
                    return false;
                }
                menuTotalSum += sumPrice;
            }
            return true;
        }
        static public bool TryGetDishPrice(Dish dish, PriceKurant priceKurant, out double sumPrice)
        {
            sumPrice = default;
            foreach (string key in dish.Keys)
            {
                if (!priceKurant.TryGetProductPrice(key, out double poductPrice))
                {
                    sumPrice = default;
                    return false;
                }
                sumPrice += poductPrice * dish[key];
            }
            return true;
        }
        static public Menu CreateMenuFromFile(string nameFile)
        {
            Menu menu = new Menu();

            if (File.Exists(nameFile))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(nameFile))
                    {
                        Dish? currentDish = null;
                        bool currentDishCorrect = true;
                        while (!sr.EndOfStream)
                        {
                            string? currentLine = sr.ReadLine();
                            if (currentLine == null)
                            {
                                continue;
                            }

                            //Gap between dishes
                            if (String.IsNullOrEmpty(currentLine))
                            {
                                //Окончание меню
                                if (currentDish != null)
                                {
                                    if (currentDishCorrect)
                                    {
                                        menu.Add(currentDish);
                                    }
                                    currentDish = null;
                                    currentDishCorrect = true;
                                }
                                continue;
                            }

                            string[] parties = currentLine.Split(',', StringSplitOptions.TrimEntries);
                            if (currentDish == null)
                            {
                                if (parties.Length != 1)
                                {
                                    LoggerErrorAdd?.Invoke($"Line <{currentLine}> is not name of Dish>");
                                    continue;
                                }
                                currentDish = new(parties[0]);
                            }
                            else
                            {
                                if (parties.Length < 2)
                                {
                                    LoggerErrorAdd?.Invoke($"Line <{currentLine}> is the wrong ingredient>");
                                    currentDishCorrect = false;
                                    continue;
                                }
                                string valueStr = (parties.Length == 2) ? parties[1] : String.Join(',', parties[1..]);
                                if (!float.TryParse(valueStr, out float value))
                                {
                                    LoggerErrorAdd?.Invoke($"Line <{currentLine}> from menu {currentDish.Name} сontains an invalid value {valueStr}>");
                                    currentDishCorrect = false;
                                    continue;
                                }
                                currentDish.Add(parties[0], value);
                            }
                        }
                        if (currentDish != null && currentDishCorrect)
                        {
                            menu.Add(currentDish);
                        }
                    }
                }
                catch (Exception e)
                {
                    LoggerErrorAdd?.Invoke($"Error> {e.Message}");
                }

            }
            return menu;
        }
        static public PriceKurant CreatePriceKurantFromFile(string nameFile)
        {
            PriceKurant priceKurant = new();

            if (File.Exists(nameFile))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(nameFile))
                    {
                        while (!sr.EndOfStream)
                        {
                            string? currentLine = sr.ReadLine();
                            if (currentLine == null)
                            {
                                continue;
                            }

                            string[] parties = currentLine.Split('-', StringSplitOptions.TrimEntries);
                            if (parties.Length != 2)
                            {
                                LoggerErrorAdd?.Invoke($"Line <{currentLine}> is the wrong price>");
                                continue;
                            }
                            if (!float.TryParse(parties[1], out float value))
                            {
                                LoggerErrorAdd?.Invoke($"Line <{currentLine}> from Ingredient {parties[0]} сontains an invalid value {parties[1]}>");
                                continue;
                            }
                            priceKurant.Add(parties[0], value);
                        }
                    }
                }
                catch (Exception e)
                {
                    LoggerErrorAdd?.Invoke($"Error> {e.Message}");
                }
            }
            return priceKurant;
        }
        static public ExchangeRates CreateExchangeRatesFromFile(string nameFile)
        {
            ExchangeRates exchangeRates = new();

            if (File.Exists(nameFile))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(nameFile))
                    {
                        while (!sr.EndOfStream)
                        {
                            string? currentLine = sr.ReadLine();
                            if (currentLine == null)
                            {
                                continue;
                            }

                            string[] parties = currentLine.Split('-', StringSplitOptions.TrimEntries);
                            if (parties.Length != 2)
                            {
                                LoggerErrorAdd?.Invoke($"Line <{currentLine}> is the wrong price>");
                                continue;
                            }
                            if (!float.TryParse(parties[1], out float value))
                            {
                                LoggerErrorAdd?.Invoke($"Line <{currentLine}> from Ingredient {parties[0]} сontains an invalid value {parties[1]}>");
                                continue;
                            }
                            exchangeRates.Add(parties[0], value);
                        }
                    }
                }
                catch (Exception e)
                {
                    LoggerErrorAdd?.Invoke($"Error> {e.Message}");
                }
            }
            return exchangeRates;
        }
        static public Order AcceptOrder(Menu menu)
        {
            Order order = new();

            if (ExtInputAction == null)
            {
                LoggerErrorAdd?.Invoke("No input source (ExtInputAction)");
                return order;
            }

            while (true)
            {
                ExtDisplayAction?.Invoke("");
                ExtDisplayAction?.Invoke("Choose a dish. To finish, enter .");
                for (int i = 0; i < menu.Length; i++)
                {
                    ExtDisplayAction?.Invoke($"{i+1} - {menu[i].Name}");
                }

                //Choice of dish
                string strInput = ExtInputAction?.Invoke() ?? "";
                ExtDisplayAction?.Invoke("You entered: " + strInput);

                if (strInput == ".")
                {
                    break;
                }

                if (!int.TryParse(strInput, out int indexDish)
                    || indexDish < 1
                    || indexDish > menu.Length)
                {
                    ExtDisplayAction?.Invoke("Wrong dish number");
                    continue;
                }

                //quantity input
                ExtDisplayAction?.Invoke("");
                ExtDisplayAction?.Invoke("Enter Quantity");
                strInput = ExtInputAction?.Invoke() ?? "";
                ExtDisplayAction?.Invoke("You entered: " + strInput);
                
                if (strInput == ".")
                {
                    break;
                }

                if (!int.TryParse(strInput, out int value)
                    || indexDish < 1)
                {
                    ExtDisplayAction?.Invoke("Wrong Quantity");
                    continue;
                }

                order.Add(menu[indexDish - 1], value);
            }

            return order;

        }
        static public string GetOrderCost(Order order, PriceKurant priceKurant, KeyValuePair<string, double> rate)
        {
            StringBuilder sb = new();
            Dictionary<string, double> composition = new();

            foreach (Dish dish in order.Keys)
            {
                int value = order[dish];
                foreach (string inrg in dish.Keys)
                {
                    if (!composition.ContainsKey(inrg))
                    {
                        composition[inrg] = 0;
                    }
                    composition[inrg] = dish[inrg] * value;
                }
            }

            double cost = 0;
            sb.AppendLine("");
            sb.AppendLine("Composition of the dish");
            foreach (KeyValuePair<string, double> pair in composition)
            {
                sb.AppendLine($"{pair.Key} - {String.Format("{0:0.###}", pair.Value)}");

                cost += GetPrice(pair.Key, priceKurant) / rate.Value;

            }
            sb.AppendLine($"Cost: {String.Format("{0:0.##}", cost)} {rate.Key}");

            return sb.ToString();
        }

        static public double GetPrice(string product, PriceKurant priceKurant)
        {
            double price;
            if (priceKurant.TryGetProductPrice(product, out price))
            {
                return price;
            }

            for (int i = 1; i <= numberTry; i++)
            {
                ExtDisplayAction?.Invoke("");
                ExtDisplayAction?.Invoke($"Enter the price of the {product}");
                string strInput = ExtInputAction?.Invoke() ?? "";
                ExtDisplayAction?.Invoke("You entered: " + strInput);

                if (double.TryParse(strInput, out price))
                {
                    priceKurant.Add(product, price);
                    return price;
                }
            }

            return 0;
        }
    }
}
