using HomeWork_09_1;

//Логгер помилок
Logger loggerError = new("..\\..\\..\\LogError.log");
loggerError.ExtDisplayAction = Console.WriteLine;

//для перенаправлення виводу результату
Logger Dispay = new("..\\..\\..\\result.txt");
Dispay.ExtDisplayAction = Console.WriteLine;
Dispay.WithTime = false;

//налаштування MenuService
MenuService.LoggerErrorAdd += loggerError.Add; //виводимо у файл
MenuService.LoggerErrorAdd += Console.WriteLine; //дублюємо на екран
MenuService.ExtDisplayAction = Console.WriteLine;  //виводимо результат у консоль
MenuService.ExtDisplayAction += Dispay.Add; //дублюємо у файл
MenuService.ExtInputAction = Console.ReadLine; //джерело введення

//Завантаженню меню
Menu menu = MenuService.CreateMenuFromFile("..\\..\\..\\Menu.txt");
MenuService.ExtDisplayAction("Menu");
MenuService.ExtDisplayAction(menu.ToString()??"");

//Завантаження прайсу 
PriceKurant priceKurant = MenuService.CreatePriceKurantFromFile("..\\..\\..\\Prices.txt");
MenuService.ExtDisplayAction("Price");
MenuService.ExtDisplayAction(priceKurant.ToString()??"");

//Завантаження курсу 
ExchangeRates exchangeRates = MenuService.CreateExchangeRatesFromFile("..\\..\\..\\ExchangeRates.txt");
MenuService.ExtDisplayAction("ExchangeRates");
MenuService.ExtDisplayAction(exchangeRates.ToString() ?? "");

//Оформленя заказу
MenuService.ExtDisplayAction("Input order");
Order order = MenuService.AcceptOrder(menu);
MenuService.ExtDisplayAction("\nAccepted order:");
MenuService.ExtDisplayAction(order.ToString() ?? "");

//Розразунок заказу
var Rate = exchangeRates["USD"];
string orderCost = MenuService.GetOrderCost(order, priceKurant, Rate);
MenuService.ExtDisplayAction(orderCost);