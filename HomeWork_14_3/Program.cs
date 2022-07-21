using HomeWork_14_3;

//Реалiзувати паттерн Абстрактна фабрика для продовольчих та промислових товарiв та паттерн Одинак для сутностi Склад.

//налаштування дублючання єкрану у файл
Logger Dispay = new("..\\..\\..\\result.txt");
Dispay.ExtDisplayAction = Console.WriteLine;
Dispay.WithTime = false;

Action<string>? ExtDisplayAction;
ExtDisplayAction = Dispay.Add;
ExtDisplayAction += Console.WriteLine;

////////////////////////////////////////////////////////////////////////////////////

//не зрозумiло як "гарно" зробити статичнi дегелати для класу з узагальненям.
//а делегати статичнi, тому, що бажаю виводити iнфу зi статичного методу класу.
// Не впевнена,  що встигнемо це обговорити в групі. Хіба підберемо час індивідуально
Storage<Product>.ExtDisplayAction = ExtDisplayAction;
Storage<FoodProduct>.ExtDisplayAction = ExtDisplayAction;
Storage<IndustrialProduct>.ExtDisplayAction = ExtDisplayAction;

//==================================Абстрактна фабрика
ExtDisplayAction("Абстрактна фабрика");
ExtDisplayAction(""); 

Storage<Product> storage = Storage<Product>.GetInstance();

IProductFactory factory;

//Додавання объектiв FoodProduct
ExtDisplayAction("Додавання у storage через фабрику FoodProductFactory()");
factory = new FoodProductFactory();
storage.Add(factory.ReadFromString("FoodProduct_1"));
storage.Add(factory.ReadFromString("FoodProduct_2"));

//Додавання объектiв FoodProduct
ExtDisplayAction("Додавання у storage через фабрику IndustrialProductFactory()");
factory = new IndustrialProductFactory();
storage.Add(factory.ReadFromString("IndustrialProduct_1"));
storage.Add(factory.ReadFromString("IndustrialProduct_2"));

//
storage.ShowAll("Продукти у  storage");
ExtDisplayAction("");

//==================================Тестування Одинака
ExtDisplayAction("Тестування Одинака");
ExtDisplayAction("");

//там для одинока неможна
//Storage<IProduct> storage3 = new();
ExtDisplayAction("Якщо клас з патерном Одинак, то не можна так: Storage<IProduct> storage = new()");
ExtDisplayAction("");

ExtDisplayAction("Створення Storage<IndustrialProduct> storage1:");
Storage<IndustrialProduct> storage1 = Storage<IndustrialProduct>.GetInstance();
ExtDisplayAction("storage1.Guid=" + storage1.Guid);
ExtDisplayAction("");

ExtDisplayAction("Створення Storage<IndustrialProduct> storage2:");
Storage<IndustrialProduct> storage2 = Storage<IndustrialProduct>.GetInstance();
ExtDisplayAction("storage2.Guid=" + storage2.Guid);
ExtDisplayAction("");

ExtDisplayAction("Створення Storage<FoodProduct> storage3:");
Storage<FoodProduct> storage3 = Storage<FoodProduct>.GetInstance();
ExtDisplayAction("storage3.Guid=" + storage3.Guid);
ExtDisplayAction("");

ExtDisplayAction("порiвняємо (storage1.Guid == storage2.Guid) = " + (storage1.Guid == storage2.Guid));
ExtDisplayAction("спiваадають - це той самий одинак");
ExtDisplayAction("");

ExtDisplayAction("порiвняємо (storage1.Guid == storage3.Guid) = " + (storage1.Guid == storage3.Guid));
ExtDisplayAction("рiзнi - це рiзнi одинаки");
ExtDisplayAction("");

//А що з серіалізацією?


