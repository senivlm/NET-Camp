using Math;


//Vector vec = new(15);
//vec.InitFromString("324 44 2 3 3 1 23 3 4");
//Console.WriteLine(vec);

Vector vec = new(15);
//vec.NotifyStep += Console.WriteLine;
for (int i = 0; i < 100; i++)
{
    vec.InitShuffle();
    Console.WriteLine(vec);
    vec.SortSplitMerge(SortingDirection.ASC);
    Console.WriteLine($"vec = {vec} IsSorted = {vec.IsSorted(SortingDirection.ASC)}");
}


