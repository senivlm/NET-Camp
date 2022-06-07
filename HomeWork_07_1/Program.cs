//Здійснити зміни у класі Storage. Опрацювати виняткові ситуації, що можуть виникати при наповненні даними з файлу.
//1. У випадку не існування файлу надати кілька спроб користувачу змінити файл завантаження.
//2. При неправильному форматі даних для зчитування вивести інформацію в файл-журнал реєстрації помилок з фіксацією дату та часу перевірки. 
//3. Некоректні дані не вносити до колекції. 
//4. Вважати, що всі назви товарів мають бути з великої літери. У випадку, якщо у файл назва занесена з малої літери, дане не вважати некоректним,
//   а замінити першу  літеру та додати товар до колекції. 
//5. Створити метод, який надає можливість аналізувати журнал реєстрації та змінювати дані, які попали в журнал пізніше за задану користувачем дату.
//   Продумати архітектуру розв’язку цієї проблеми. Визначити, в якому класі реалізувати код.

using HomeWork_07_1;

//Логгер помилок
Logger loggerError = new("..\\..\\..\\LogError.log");
loggerError.ExtDisplayAction = Console.WriteLine;

//Логгер успіху
Logger loggerSuccess = new("..\\..\\..\\LogSuccess.log");
loggerSuccess.ExtDisplayAction = Console.WriteLine;

//Сховище
Storage storage = new Storage(20);
storage.ExtDisplayAction = Console.WriteLine;
storage.ExtInputAction = Console.ReadLine;
storage.LoggerErrorAdd += loggerError.Add;
storage.LoggerSuccessAdd += loggerSuccess.Add;

//Читання з файлу
storage.ReadProductsFromFile("..\\..\\..\\", "Input.txt");
storage.ShowAll();

//Вивід помилок на екран
loggerError.ExtDisplayAction = Console.WriteLine; 
loggerError.ShowLog(new DateTime(2022,01,01));

//Надання користовачеві ввести дані повторно
loggerError.ExtDisplayAction = storage.RepeatReadFromString;
loggerError.ShowLog(new DateTime(2022, 01, 01));
