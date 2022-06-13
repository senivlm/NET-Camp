//Перевантажити оператори +,  - для класу Обліку електроенергії. Оператор + перевантажити, створюючи спільну колекцію,
//з об’єднаною інформацією з першого та другого операнда.
//Операцію мінус протрактувати як зміну першого операнда з вилученням з нього інформації про квартири, які є в другому операнді.
//Інформацію про квартири вважати однаковою, якщо збігається номер квартири та власник.


//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//Так як моя структура обліку електроенегії увявляю собою БД,
//а також власники квартири і показники це не данні одного об'екту,
//то це завдання не можу продемонструвати на Обліку і демонструю на Storage
//!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

using HomeWork_08_1;

//Логгер помилок
Logger loggerError = new("..\\..\\..\\LogError.log");
loggerError.ExtDisplayAction = Console.WriteLine;

//Логгер успіху
Logger loggerSuccess = new("..\\..\\..\\LogSuccess.log");
loggerSuccess.ExtDisplayAction = Console.WriteLine;

//для перенаправлення виводу результату
Logger Dispay = new("..\\..\\..\\DisplayResult.log");
Dispay.ExtDisplayAction = Console.WriteLine;

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
Storage storage1 = storageA + storageB;
storage1.ShowAll("storage1 = storageA + storageB");

//2. Товари, які  є спільними в обох складах.
Storage storage2 = storageA - storageB;
storage2.ShowAll("storage2 = storageA - storageB");

//3. Спільний список товарів, які є на обох складах, без повторів елементів.
Storage storage3 = storageA & storageB;
storage3.ShowAll("storage3 = storageA & storageB");