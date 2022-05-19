using NET_CAMP_HomeWork_s1_01;

Console.WriteLine("Первіремо шо відпрацюе метод CalculateBuying() навідь при такому створенню");
Buy Buy1 = new Buy { Volume = 23,  ProductItem = new Product("Item2", 12, 33) };
Console.WriteLine($"Вага Buy1 {Buy1.TotalWeight}");

Product prod = new Product("Item1", 12.3f, 33.4f);
Buy buy = new Buy(prod, 12);

Console.WriteLine("Створені об'екті");
Check.ShowAbout(prod);
Check.ShowAbout(buy);

Console.WriteLine("Змінемо кількість");
buy.Volume = 1;
Check.ShowAbout(buy);

Console.WriteLine("Перевіремо від'емну кількість");
buy.Volume = -1;
Check.ShowAbout(buy);
