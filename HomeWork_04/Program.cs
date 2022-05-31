using HomeWork_04;
//Робота загалом дуже хороша!
// Слід перехоплювати винятки.
Vector vectorForTest6 = new(2, 1, 4, 4, 4, 4, 4, 4, 4, 5, 3);
vectorForTest6.NotifyStep += Console.WriteLine;

Console.WriteLine($"Vector: \n{vectorForTest6} IsSorted={vectorForTest6.IsSorted(SortingDirection.ASC)}");
vectorForTest6.SortQuick(SortingDirection.ASC, TypeQuickSort.LEFT);

Console.WriteLine($"Vector: \n{vectorForTest6} IsSorted={vectorForTest6.IsSorted(SortingDirection.ASC)}");
Console.WriteLine();


//Vector vectorForTest7 = new(15);
//for (int i = 0; i < 10000; i++)
//{
//    vectorForTest7.InitRandom(1, 7);
//    Vector VectorTMP = new(vectorForTest7);
//    vectorForTest7.SortQuick(SortingDirection.ASC, TypeQuickSort.RIGHT);
//    if (!vectorForTest7.IsSorted(SortingDirection.ASC))
//    {
//        Console.WriteLine($"Vector: \n{VectorTMP}");
//    }
//    if ((i / 100) * 100 == i)
//    {
//        Console.WriteLine(i);
//    }
//}

//Vector vectorForTest7 = new(4, 4, 4, 4, 4, 4, 4);
//Vector vectorForTest7 = new(2, 1, 4, 4, 4, 4, 4, 4, 4, 5, 3);
//vectorForTest7.NotifyStep += Console.WriteLine;

//Console.WriteLine($"Vector: \n{vectorForTest7}");
//vectorForTest7.SortQuick(SortingDirection.ASC, TypeQuickSort.CENTRUM);

//Console.WriteLine($"Vector: \n{vectorForTest7}");
//Console.WriteLine();
