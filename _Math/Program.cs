using Math;


Vector vec = new(15);
vec.NotifyStep += Console.WriteLine;

vec.InitFromString("2 6 3 12 9 5 11 14 7 8 10 1 13 4 15");
//vec.InitFromString("2 2 2 2 2 2 2 2 2 2 2 2 2 2 2");
//vec.SaveToFile("array.txt");

//Vector vec2 = new(15);
//vec2.InitFromFile("array.txt");
//Console.WriteLine(vec2);

//vec.InitShuffle(); 
Console.WriteLine(vec);
vec.SortSplitMerge(SortingDirection.ASC);
Console.WriteLine($"vec = {vec} IsSorted = {vec.IsSorted(SortingDirection.ASC)}");

//Vector vec = new(15);
////vec.NotifyStep += Console.WriteLine;
//for (int i = 0; i < 100; i++)
//{
//    vec.InitShuffle();
//    Console.WriteLine(vec);
//    vec.SortHeap(SortingDirection.ASC);
//    Console.WriteLine($"vec = {vec} IsSorted = {vec.IsSorted(SortingDirection.ASC)}");
//}


