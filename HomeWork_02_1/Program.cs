using NET_CAMP_HomeWork_s1_02_01;

Product prod = new("ddddd");
Console.WriteLine(prod);

Storage storage = new(5);

storage.Add(new Product("ddddd1", 12, 13));
storage.Add(new Product("ddddd2", 13, 14));
storage.Add(new Meat("ddddd3", Category.TopGrade, Kind.Veal, 14, 15));
storage.Add(new Product("ddddd4", 15, 16));
storage.Add(new Product("ddddd5", 16, 17));
storage.Add(new Product("ddddd6", 17, 18));

storage.ShowAll();
storage.ShowMeat();

Console.WriteLine("Input percent");
string? str = Console.ReadLine();
float percent;
if (float.TryParse(str, out percent)) 
{
    storage.SetPrice(percent);
    storage.ShowAll();
}

Console.WriteLine($"First product: {storage[0]}"); 



