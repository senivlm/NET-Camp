using HomeWork_03;

try 
{
    //Додати в клас Vector метод, який перевіряє, чи поле є паліндромом.
    Vector vectorForTest1 = new(19);
    vectorForTest1.InitRandom(1, 10);
    bool isPalindrome = vectorForTest1.IsPalindrome();
    Console.WriteLine($"Vector: {vectorForTest1}");
    Console.WriteLine(isPalindrome ? "It is a Palindrome" : "It isn't a Palindrome");
    Console.WriteLine();

    //Додати в клас Vector метод, який реверсує елементи масиву. Створити власний метод. А також показати використання стандартного методу.
    Vector vectorForTest2 = new(17);
    vectorForTest2.InitRandom(1, 10);
    Console.WriteLine($"Original           Vector: {vectorForTest2}");
    vectorForTest2.Reverse(ImplementationMethod.own);
    Console.WriteLine($"Reversed           Vector: {vectorForTest2}");
    vectorForTest2.Reverse(ImplementationMethod.standart);
    Console.WriteLine($"Return to original Vector: {vectorForTest2}");
    Console.WriteLine();

    //Додати в клас Vector метод, який в масиві знаходить найдовшу підпослідовність однакових чисел.
    Vector vectorForTest3 = new(22);
    vectorForTest3.InitRandom(1, 4);
    Console.WriteLine($"Vector: {vectorForTest3}");
    Pair? LongestSubSequence = vectorForTest3.GetLongestSubSequence();
    Console.WriteLine($"First Longest SubSequence is {LongestSubSequence}");
    Console.WriteLine();

    //У класі Matrix створити метод, який заповнює квадратну матрицю діагональною змійкою,
    //параметром методу має бути напрям початкового повороту змійки (вправо, чи вниз), заданий змінною типу Enum.
    Matrix matrix4 = new(4);
    matrix4.InitDiagonalSnake(Direction.right);
    Console.WriteLine($"Matrix right: \n{matrix4}");
    Console.WriteLine();
    
    Matrix matrix5 = new(5);
    matrix5.InitDiagonalSnake(Direction.down);
    Console.WriteLine($"Matrix down: \n{matrix5}");
    Console.WriteLine();

    //Оптимізувати метод InitShufle класу Vector, створений на занятті.
    Vector vectorForTest6 = new(22);
    vectorForTest6.InitShuffle();
    Console.WriteLine($"Vector: {vectorForTest6}");
    Console.WriteLine();

}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
