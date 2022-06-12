using HomeWork_08_2;

//Дано текстовий файл зі статистикою відвідування сайту за тиждень. Кожен рядок містить ip адресу, час і назву дня тижня. 
//(наприклад, 139.18.150.126 23:12:44 sunday).
//Для кожного ip вкажіть кількість відвідувань на тиждень, найбільш популярний день тижня,
//найбільш популярний відрізок часу довжиною в одну годину.
//Знайдіть також найбільш популярний відрізок часу в добі довжиною одну годину в цілому для сайту.
//Продумайте, як оптимально здійснити повторювану дію для різних даних.


//Файл з даними дляаналізу
string fileForAnaliz = "..\\..\\..\\Connect.log";

//Виклик одноразово для створення файлу
if (!File.Exists(fileForAnaliz))
{
    GeneratorOfLogs.Create(fileForAnaliz, 10000);
}

//Логгер помилок
Logger loggerError = new("..\\..\\..\\LogError.log");
loggerError.ExtDisplayAction = Console.WriteLine;

//для перенаправлення виводу результату
Logger Dispay = new("..\\..\\..\\DisplayResult.log");
Dispay.ExtDisplayAction = Console.WriteLine;

//Налаштування аналізатору логів
LogAnalyzer logAnalizer = new LogAnalyzer();
logAnalizer.LoggerErrorAdd += loggerError.Add;
logAnalizer.LoggerErrorAdd += Console.WriteLine;
logAnalizer.ExtDisplayAction = Console.WriteLine; //виводимо результат у консоль
logAnalizer.ExtDisplayAction += Dispay.Add; //дублюємо у файл

//Завантажимо файл для аналізу
logAnalizer.Load(fileForAnaliz);

//Розділимо по IP
Dictionary<object, LogAnalyzer> dicSplitByIP = logAnalizer.SplitByField((LogRecord record) => record.IPAdress);

//Статистика по IP
foreach (KeyValuePair<object, LogAnalyzer> recDic in dicSplitByIP)
{
    LogAnalyzer logAnalizerIP = recDic.Value;
    logAnalizerIP.ShowStat(recDic.Key.ToString()??"");
}

//Статистика повна
logAnalizer.ShowStat("All");









