//Для класу Matrix, створеного раніше, реалізувати інтерфейс IEnumarable таким чином,
//щоб за допомогою оператора foreach була можливість обходу довільної матриці
//у вигляді горизонтальної змійки (першу стрічку розглядаємо від початку до кінця, другу від кінця до початку і т.д.),
//а також діагональної змійки.

using HomeWork_10_2;
using System.Text;

//Є БАГАТО ЗАПИТАНЬ !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
//1. яка різниця між IEnumerable<int> і IEnumerable
//2. при реалізации IEnumerable<int> треба реалізувати "IEnumerator IEnumerable.GetEnumerator()" коли він використовуеються?
//3. Якщо реалізувати IEnumerable, то ми реалізуємо методи що повертать IEnumerator, а у foreach ми можемо обирати
//    потрібний IEnumerable. Не зміг пов'язати їх між собою і задублював код.
//4. Як у foreach обирати потрібний IEnumerator, а не IEnumerable 

try
{

    StringBuilder sb = new();

    //налаштування дублючання єкрану у файл
    Logger Dispay = new("..\\..\\..\\result.txt");
    Dispay.ExtDisplayAction = Console.WriteLine;
    Dispay.WithTime = false;
    Matrix matrix = new(4);

    Action<string>? ExtDisplayAction;
    ExtDisplayAction = Dispay.Add;
    ExtDisplayAction += Console.WriteLine;

    //DiagonalSnake
    ExtDisplayAction?.Invoke("DiagonalSnake");
    matrix.InitDiagonalSnake(Direction.RIGHT);
    ExtDisplayAction?.Invoke(matrix.ToString()??"" + "\n");
    sb.Clear();

    foreach (int value in matrix.GetEnumerableDiagonalSnake(Direction.RIGHT))
    {
        sb.Append(value + " ");
    }
    ExtDisplayAction?.Invoke(sb.ToString() + "\n");

    //HorizontalSnake
    ExtDisplayAction?.Invoke("HorizontalSnake");
    matrix.InitHorizontalSnake();
    ExtDisplayAction?.Invoke(matrix.ToString()?? "" + "\n");
    sb.Clear();

    foreach (int value in matrix.GetEnumerableHorizontalSnake())
    {
        sb.Append(value + " ");
    }
    ExtDisplayAction?.Invoke(sb.ToString());

}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

