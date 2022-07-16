using HomeWork_14_3;

//Реалізувати паттерн Абстрактна фабрика для продовольчих та промислових товарів та паттерн Одинак для сутності Склад.

//налаштування дублючання єкрану у файл
Logger Dispay = new("..\\..\\..\\result.txt");
Dispay.ExtDisplayAction = Console.WriteLine;
Dispay.WithTime = false;

Action<string>? ExtDisplayAction;
ExtDisplayAction = Dispay.Add;
ExtDisplayAction += Console.WriteLine;

////////////////////////////////////////////////////////////////////////////////////

//не зрозуміло як "гарно" зробити статичні дегелати для класу з узагальненям.
//а делегати статичні, тому, що бажаю виводити інфу зі статичного методу класу
Storage<IProduct>.ExtDisplayAction = ExtDisplayAction;
Storage<IWeightProduct>.ExtDisplayAction = ExtDisplayAction;

//Абстрактна фабрика
//нацей час не створена

//Тестування Одинака

//там для одинока неможна
//Storage<IProduct> storage3 = new();
ExtDisplayAction("Якщо клас з патерном Одинак, то не можна так: Storage<IProduct> storage = new()");
ExtDisplayAction("");

ExtDisplayAction("Створення Storage<IProduct> storage1:");
Storage<IProduct> storage1 = Storage<IProduct>.GetInstance();
ExtDisplayAction("storage1.Guid=" + storage1.Guid);
ExtDisplayAction("");

ExtDisplayAction("Створення Storage<IProduct> storage2:");
Storage<IProduct> storage2 = Storage<IProduct>.GetInstance();
ExtDisplayAction("storage2.Guid=" + storage2.Guid);
ExtDisplayAction("");

ExtDisplayAction("Створення Storage<IWeightProduct> storage3:");
Storage<IWeightProduct> storage3 = Storage<IWeightProduct>.GetInstance();
ExtDisplayAction("storage3.Guid=" + storage3.Guid);
ExtDisplayAction("");

ExtDisplayAction("порівняємо (storage1.Guid == storage2.Guid) = " + (storage1.Guid == storage2.Guid));
ExtDisplayAction("співаадають - це той самий одинак");
ExtDisplayAction("");

ExtDisplayAction("порівняємо (storage1.Guid == storage3.Guid) = " + (storage1.Guid == storage3.Guid));
ExtDisplayAction("різні - це різні одинаки");
ExtDisplayAction("");



