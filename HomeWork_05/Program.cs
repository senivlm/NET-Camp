using HomeWork_05;
// При виклику Merge  у Вас є перевищення ліміту пам'яті.
//Підготовка файлу
string nameFile = "arrayHW_05.txt";
Vector vec1 = new(15);
vec1.InitShuffle();
vec1.SaveToFile(nameFile);

//Змінити метод сортування злиттям, враховуючи обмеження,
//що елементи для сортування розташовані в файлі і в програмі можна використовувати тільки масиви,
//кількість елементів яких вдвічі менша за кількість елементів в файлі.
Vector vec2 = new(15);
vec2.NotifyStep += Console.WriteLine; 
vec2.InitFromFile(nameFile);
Console.WriteLine(vec2);
vec2.SortSplitMerge(SortingDirection.ASC);
Console.WriteLine($"vec2 = {vec2} IsSorted = {vec2.IsSorted(SortingDirection.ASC)}");
Console.WriteLine();

//Реалізувати в класі Vector метод пірамідального сортування.
vec2.InitFromFile(nameFile);
Console.WriteLine(vec2);
vec2.SortHeap(SortingDirection.ASC);
Console.WriteLine($"vec2 = {vec2} IsSorted = {vec2.IsSorted(SortingDirection.ASC)}");
