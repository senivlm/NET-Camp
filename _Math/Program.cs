using Math;



Vector vec = new(15);
//vec.NotifyStep += Console.WriteLine;
vec.InitRandom(0, 9);
//Console.WriteLine(vec);
//vec.SortCounting(SortingDirection.DESC); //new IntDescComparer()
//Console.WriteLine(vec);

foreach(int i in vec)
{
    Console.WriteLine(i);
}

//int[] arr = new int[] { 1, 5, 2, 8, 2, 6, 1 };
//Array.Sort(arr, new IntDescComparer());
//for ( int i = 0; i < arr.Length; i++)
//{
//    Console.Write(arr[i]);
//}

//vec.InitFromString("2 6 3 12 9 5 11 14 7 8 10 1 13 4 15");
//vec.InitFromString("2 2 2 2 2 2 2 2 2 2 2 2 2 2 2");
//vec.SaveToFile("array.txt");

//Vector vec2 = new(15);
//vec2.InitFromFile("array.txt");
//Console.WriteLine(vec2);

//vec.InitShuffle(); 
//Console.WriteLine(vec);
//vec.SortSplitMerge(SortingDirection.ASC);
//Console.WriteLine($"vec = {vec} IsSorted = {vec.IsSorted(SortingDirection.ASC)}");

//Vector vec = new(15);
////vec.NotifyStep += Console.WriteLine;
//for (int i = 0; i < 100; i++)
//{
//    vec.InitShuffle();
//    Console.WriteLine(vec);
//    vec.SortHeap(SortingDirection.ASC);
//    Console.WriteLine($"vec = {vec} IsSorted = {vec.IsSorted(SortingDirection.ASC)}");
//}


