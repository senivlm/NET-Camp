//Порівняти 2 об’єкти класу Склад і визначити наступні результати:
//1. Товари є в першому складі і немає в другому.
//2. Товари, які  є спільними в обох складах.
//3. Спільний список товарів, які є на обох складах, без повторів елементів.

using HomeWork_08_3;

//Логгер помилок
Logger loggerError = new("..\\..\\..\\LogError.log");
loggerError.ExtDisplayAction = Console.WriteLine;

//Логгер успіху
Logger loggerSuccess = new("..\\..\\..\\LogSuccess.log");
loggerSuccess.ExtDisplayAction = Console.WriteLine;

//для перенаправлення виводу результату
Logger Dispay = new("..\\..\\..\\DisplayResult.log");
Dispay.ExtDisplayAction = Console.WriteLine;
Dispay.WithTime = false;

//Сховище 1
Storage storageA = new();
storageA.ExtDisplayAction = Console.WriteLine;  //виводимо результат у консоль
storageA.ExtDisplayAction += Dispay.Add; //дублюємо у файл
//storageA.ExtInputAction = Console.ReadLine;
storageA.LoggerErrorAdd += loggerError.Add;
storageA.LoggerSuccessAdd += loggerSuccess.Add;
//Читання з файлу
storageA.ReadProductsFromFile("..\\..\\..\\", "Input1.txt");

//Сховище 2
Storage storageB = new();
storageB.ExtDisplayAction = Console.WriteLine;
storageB.ExtInputAction = Console.ReadLine;
storageB.LoggerErrorAdd += loggerError.Add;
storageB.LoggerSuccessAdd += loggerSuccess.Add;
//Читання з файлу
storageB.ReadProductsFromFile("..\\..\\..\\", "Input2.txt");

storageA.ShowAll("storageA");
storageB.ShowAll("storageB");

//1. Товари є в першому складі і немає в другому.
Storage storage1 = storageA - storageB;
storage1.ShowAll("storageA - storageB");

//2. Товари, які  є спільними в обох складах.
Storage storage2 = storageA & storageB;
storage2.ShowAll("storageA & storageB");

//3. Спільний список товарів, які є на обох складах, без повторів елементів.
Storage storage3 = storageA.Intersect(storageB);
storage3.ShowAll("storageA.Intersect(storageB)");



