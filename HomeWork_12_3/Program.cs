
//У стрічці поданий правильний запис математичної формули, який включає дійсні числа, операції +,-,*,^,\, та дужки,
//а також функції cos(), sin().Використовуючи механізм польського запису у постфіксній формі, порахувати значення виразу.

using HomeWork_12_3;

//налаштування дублючання єкрану у файл
Logger Dispay = new("..\\..\\..\\result.txt");
Dispay.ExtDisplayAction = Console.WriteLine;
Dispay.WithTime = false;

Action<string>? ExtDisplayAction;
ExtDisplayAction = Dispay.Add;
ExtDisplayAction += Console.WriteLine;

//////////////////////////////////////////////////////////////////

Calculator calc = new();
calc.NotifyStep += ExtDisplayAction;

List<string> listForCalc = new();
listForCalc.Add("3 +4 * 2 / (1-5) ^ 2"); 
listForCalc.Add("1,5-2.5*3(3-1)");
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
