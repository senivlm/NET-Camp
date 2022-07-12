
//У стрічці поданий правильний запис математичної формули, який включає дійсні числа, операції +,-,*,^,\, та дужки,
//а також функції cos(), sin().Використовуючи механізм польського запису у постфіксній формі, порахувати значення виразу.
// Не знайшла 13 задачу.
using HomeWork_12_3;

//налаштування дублючання єкрану у файл
Logger Dispay = new("..\\..\\..\\result.txt");
Dispay.ExtDisplayAction = Console.WriteLine;
Dispay.WithTime = false;
// Чому з великої літери?12 і 13 краще суміститити.
Action<string>? ExtDisplayAction;
ExtDisplayAction = Dispay.Add;
ExtDisplayAction += Console.WriteLine;

//////////////////////////////////////////////////////////////////

Calculator calc = new();
calc.NotifyStep += ExtDisplayAction;

List<string> listForCalc = new();
listForCalc.Add("3 +4 * 2 / (1-5) ^ 2"); 
// За умовою має бути правильний запис формули
listForCalc.Add("1,5-2.5*3(3-1)");
//За умовою має бути правильний запис формули/ Цю формулу мушу обговорити.
listForCalc.Add("-2(cos(0)*2+1*tan(4*sin(2^3))^3)");

foreach(string item in listForCalc)
{
    calc.InputString = item;
    ExtDisplayAction?.Invoke("");
    
    try
    {
        ExtDisplayAction?.Invoke("Original= " + calc.InputString);
        ExtDisplayAction?.Invoke("Poland= " + calc.Trasform());
        ExtDisplayAction?.Invoke("Result= " + calc.Calc());
    }
    catch (Exception ex)
    {
        ExtDisplayAction?.Invoke(ex.Message);
    }
}
