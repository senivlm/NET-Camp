using Math;
using System.Collections;

//Vector ver = new Vector(3);
//ver.InitShuffle();
//try
//{
//    Console.WriteLine(ver[10]);
//}
//catch (Exception e)
//{
//    Console.WriteLine(e.Message);
//}

//ArrayList arrayList = new ArrayList();
//arrayList.Add(4);
//arrayList.Add("ddd");
//arrayList.Add(ver);

SortedSet<int> ar = new(int.);
ar.Add(1);
ar.Add(4);
ar.Add(4);
ar.Add(3);

foreach (int i in ar)
{
    Console.WriteLine(i);   
}



//foreach (object i in arrayList)
//{
//    Console.WriteLine(i.ToString());
//}




//Matrix matr = new(15, 6);
////matr.InitRandom(1,9);

////using(StreamWriter stream = new StreamWriter("matrix.txt"))
////{
////    matr.SaveToStream(stream);
////}

//using (StreamReader stream = new StreamReader("matrix.txt"))
//{
//    matr.InitFronStream(stream);
//}
//Console.WriteLine(matr.ToString());


//Vector vec = new(15);
//vec.NotifyStep += Console.WriteLine;

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


