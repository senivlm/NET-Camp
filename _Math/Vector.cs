using System.Collections;

namespace Math
{
    internal class Vector : IEnumerable
    {
        #region fields
        private readonly int[] array;
        #endregion

        #region properties
        public int Lenght => array.Length;
        #endregion

        #region events
        public event Action<string>? NotifyStep;
        #endregion

        #region constructors
        public Vector(int n)
        {
            if (n <= 0)
            {
                throw new ArgumentException("Vector size is 0 or less");
            }
            array = new int[n];
        }
        public Vector(params int[] arrIn) : this(arrIn.Length)
        {
            //I understand that Length=1 impossible because it will be other constructor
            InitFix(arrIn);
        }
        public Vector(Vector arrayIn) : this((arrayIn == null) ? 0 : arrayIn.Lenght)
        {
            if (arrayIn != null)
            {
                for (int i = 0; i < arrayIn.Lenght; i++)
                {
                    this[i] = arrayIn[i];
                }
            }
        }
        #endregion

        #region indexers
        public int this[int index]
        {
            get
            {
                if (index >= array.Length || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                return array[index];
            }
            set
            {
                if (index >= array.Length || index < 0)
                {
                    throw new IndexOutOfRangeException();
                }
                array[index] = value;
            }
        }
        #endregion

        #region overrided_methods
        public override string? ToString()
        {
            var sb = new System.Text.StringBuilder();
            for (int i = 0; i < array.Length; i++)
            {
                sb.Append($"{array[i]} ");
            }
            return sb.ToString();
        }
        public override bool Equals(object? obj)
        {
            if (!(obj is Vector))
            {
                return false;
            }

            Vector tmp = (Vector)obj;
            if (this.Lenght != tmp.Lenght)
            {
                return false;
            }

            for (int i = 0; i < this.Lenght; i++)
            {
                if (this[i] != tmp[i])
                {
                    return false;
                }
            }
            return true;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        //public static int operator +(Vector a, Vector b)
        //{
        //    return a.Sum() + b.Sum();
        //}
        public static Vector operator +(Vector a, Vector b)
        {
            int lengtRes = (a.Lenght > b.Lenght) ? a.Lenght : b.Lenght;
            Vector c = new(lengtRes);
            for (int i = 0; i < a.Lenght; i++)
            {
                c[i] = a[i];
            }
            for (int i = 0; i < b.Lenght; i++)
            {
                c[i] += b[i];
            }
            return c;
        }
        public static Vector operator +(Vector a, int n)
        {
            Vector c = new(a.Lenght);
            for (int i = 0; i < a.Lenght; i++)
            {
                c[i] = a[i] + n;
            }
            return c;
        }
        public static bool operator >(Vector a, Vector b)
        {
            return (a.Lenght > b.Lenght);
        }
        public static bool operator <(Vector a, Vector b)
        {
            return (a.Lenght < b.Lenght);
        }
        
        public static explicit operator int(Vector a)
        {
            if (a.Lenght == 0)
            {
                return 0;
            }
            else
            {
                return a[0];
            }
        }
        
        public static implicit operator Vector(int t)
        {
            Vector b = new(1);
            b[0] = t;
            return b;
        }
        public IEnumerator GetEnumerator()
        {
            return array.GetEnumerator();
        }
        #endregion

        #region init_methods
        public void InitRandom(int a, int b)
        {
            Random ran = new();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = ran.Next(a, b);
            }
        }
        public void InitShuffle()
        {
            // variant 1 Extra random number generation
            //Random ran = new();
            //for (int i = 0; i < array.Length; i++)
            //{
            //    while (true)
            //    {
            //        int nom = ran.Next(1, array.Length + 1);
            //        if (!array.Contains(nom))
            //        {
            //            array[i] = nom;
            //            break;
            //        }
            //    }
            //}

            // variant 2 No extra random number generation
            Random ran = new();
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = i + 1;
            }
            for (int i = 0; i < array.Length; i++)
            {
                int index = ran.Next(0, array.Length);
                (array[i], array[index]) = (array[index], array[i]);
            }
        }
        public void InitFix(params int[] ListNumbers)
        {
            int indexFill = (array.Length < ListNumbers.Length) ? array.Length : ListNumbers.Length;
            for (int i = 0; i < indexFill; i++)
            {
                array[i] = ListNumbers[i];
            }
            for (int i = indexFill; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }
        public void InitFromString(string? str, string separator = " ")
        {
            int indexFill = 0;
            if (str != null)
            {
                string[] masStr = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                foreach (string s in masStr)
                {
                    if (indexFill >= array.Length)
                    {
                        break;
                    }

                    Int32.TryParse(s, out array[indexFill]);
                    indexFill++;

                }
            }
            for (int i = indexFill; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }
        public void InitFromFile(string fileName)
        {

            using (StreamReader stream = new(fileName))
            {
                string? line = stream.ReadLine();
                InitFromString(line);
            }

        }
        #endregion

        #region sort_methods
        public void SortBobble(IComparer<int> comparer)
        {
            if (array.Length < 2)
            {
                return;
            }

            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (comparer.Compare(array[j], array[j + 1]) > 0)
                    {
                        (array[j], array[j + 1]) = (array[j + 1], array[j]);
                        NotifyStep?.Invoke(this.ToString() ?? "");
                    }
                }
            }
        }
        public void SortCounting(SortingDirection direct = SortingDirection.ASC)
        {
            if (array.Length < 2)
            {
                return;
            }

            int maxValue = array[0];
            int minValue = array[0];
            for (int i = 1; i < array.Length; i++)
            {
                if (maxValue < array[i])
                {
                    maxValue = array[i];
                }
                if (minValue > array[i])
                {
                    minValue = array[i];
                }
            }

            int[] temp = new int[maxValue - minValue + 1];

            for (int i = 0; i < array.Length; i++)
            {
                temp[array[i] - minValue]++;
            }

            int k = 0;
            for (int i = 0; i < temp.Length; i++)
                for (int j = 0; j < temp[i]; j++)
                {
                    int index = (direct == SortingDirection.ASC) ? k : (temp.Length - k - 1);
                    array[index] = i + minValue;
                    k++;
                }
            {

            }
        }
        public void SortQuick(IComparer<int> comparer, TypeQuickSort typeQS = TypeQuickSort.CENTRUM)
        {
            if (array.Length < 2)
            {
                return;
            }

            SortQuickInternal(0, array.Length - 1, comparer, typeQS);
            return;

            void SortQuickInternal(int low, int high, IComparer<int> comparer, TypeQuickSort typeQS)
            {
                if (low < high)
                {
                    int p = Partition(low, high, comparer, typeQS);
                    if (typeQS == TypeQuickSort.RIGHT)
                    {
                        SortQuickInternal(low, p - 1, comparer, typeQS);
                        SortQuickInternal(p, high, comparer, typeQS);
                    }
                    else
                    {
                        SortQuickInternal(low, p, comparer, typeQS);
                        SortQuickInternal(p + 1, high, comparer, typeQS);
                    }
                }
            }

            int Partition(int low, int high, IComparer<int> comparer, TypeQuickSort typeQS)
            {
                int indexPivot;

                if (typeQS == TypeQuickSort.LEFT)
                {
                    indexPivot = low;
                }
                else if (typeQS == TypeQuickSort.RIGHT)
                {
                    indexPivot = high;
                }
                else
                {
                    indexPivot = (low + high) / 2;
                }

                NotifyStep?.Invoke($"Start partition. low={low} high={high} indexPivot={indexPivot}");

                int pivot = array[indexPivot];
                int i = low;
                int j = high;
                while (true)
                {
                    while (comparer.Compare(array[i], pivot) < 0) i++; //пока array[i] корректно расположен левее pivot
                    while (comparer.Compare(array[j], pivot) > 0) j--; //пока array[j] корректно расположен правее pivot
                    //if (direct == SortingDirection.ASC)
                    //{
                    //    while (array[i] < pivot) i++;
                    //    while (array[j] > pivot) j--;
                    //}
                    //else
                    //{
                    //    while (array[i] > pivot) i++;
                    //    while (array[j] < pivot) j--;
                    //}

                    if (i >= j)
                    {
                        NotifyStep?.Invoke($"Finish partition. i={i} j={j} result={j}");
                        return j;
                    }
                    (array[i], array[j]) = (array[j], array[i]);
                    NotifyStep?.Invoke($"{this.ToString() ?? ""} i={i} j={j}");

                    i++;
                    j--;
                }
            }
        }
        public void SortSplitMerge(IComparer<int> comparer)
        {
            if (array.Length < 2)
            {
                return;
            }

            SortSplitMergeInternal(0, array.Length - 1, comparer);
            return;

            void SortSplitMergeInternal(int indexStart, int indexFinish, IComparer<int> comparer)
            {

                if (indexFinish <= indexStart)
                {
                    return;
                }

                int indexMidle = (indexStart + indexFinish) / 2;
                SortSplitMergeInternal(indexStart, indexMidle, comparer);
                SortSplitMergeInternal(indexMidle + 1, indexFinish, comparer);
                Merge(indexStart, indexMidle, indexFinish, comparer);

            }

            void Merge(int indexStart1, int indexFinish1, int indexFinish2, IComparer<int> comparer)
            {
                ISerialStorage arrTmp;
               
                int size = indexFinish2 - indexStart1 + 1;
                if (size > array.Length / 2)
                {
                    arrTmp = new SerialStorageFile("arrTmp.txt");
                }
                else
                {
                    arrTmp = new SerialStorageArray(size);// int[] arrTmp = new int[indexFinish2 - indexStart1 + 1];
                }

                int i = indexStart1;
                int j = indexFinish1+1;

                //int indexArrTmp = 0;
                while (i <= indexFinish1 && j <= indexFinish2)
                {
                    if (comparer.Compare(array[j], array[i]) > 0)
                    {
                        arrTmp.Add(array[i++]); // arrTmp[indexArrTmp++] = array[i++];
                    }
                    else
                    {
                        arrTmp.Add(array[j++]); // arrTmp[indexArrTmp++] = array[j++];
                    }
                }
                while (i <= indexFinish1)
                {
                    arrTmp.Add(array[i++]); // arrTmp[indexArrTmp++] = array[i++];
                }
                while (j <= indexFinish2)
                {
                    arrTmp.Add(array[j++]); // arrTmp[indexArrTmp++] = array[j++];
                }

                arrTmp.ExportToArray(array, indexStart1); 
                //for (int n = 0; n < arrTmp.Length; n++)
                //{
                //    array[indexStart1 + n] = arrTmp[n];
                //}
                NotifyStep?.Invoke($"{this.ToString()} l={indexStart1} q={indexFinish1} r={indexFinish2}");
            }
        }
        public void SortHeap(IComparer<int> comparer)
        {

            if (array.Length < 2)
            {
                return;
            }
            
            //Create full binary tree
            for (int i = array.Length / 2 - 1; i >= 0; i--)
            {
                UpdateTreeFromParent(array.Length, i, comparer);
            }

            //Sort array
            for (int i = array.Length - 1; i >= 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                UpdateTreeFromParent(i, 0, comparer);
            }
            return;
        

            void UpdateTreeFromParent(int sizeTree, int indexParent, IComparer<int> comparer)
            {
                NotifyStep?.Invoke($"{this.ToString()} n={sizeTree} i={indexParent}");

                int indexTarget = indexParent;

                int indexChildL = 2 * indexParent + 1; // left = 2*i + 1
                int indexChildR = 2 * indexParent + 2; // right = 2*i + 2


                if (indexChildL < sizeTree 
                    && comparer.Compare(array[indexChildL], array[indexTarget]) > 0
                    )
                {
                    indexTarget = indexChildL; 
                }

                if (indexChildR < sizeTree
                    && comparer.Compare(array[indexChildR], array[indexTarget]) > 0
                    )
                {
                    indexTarget = indexChildR;
                }

                if (indexTarget != indexParent)
                {
                    (array[indexTarget], array[indexParent]) = (array[indexParent], array[indexTarget]);
                    UpdateTreeFromParent(sizeTree, indexTarget, comparer);
                }
            }
        }
        #endregion

        #region other_methods
        public void SaveToFile(string fileName)
        {

            using (StreamWriter stream = new(fileName))
            {
                stream.WriteLine(this.ToString());
            }


        }
        public bool IsPalindrome()
        {
            bool result = true;
            for (int i = 0; i < array.Length / 2; i++)
            {
                if (array[i] != array[array.Length - i - 1])
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        public bool IsSorted(SortingDirection direct)
        {
            bool result = true;
            for (int i = 0; i < array.Length - 1; i++)
            {
                if ((direct == SortingDirection.ASC && array[i] > array[i + 1])
                    || (direct == SortingDirection.DESC && array[i] < array[i + 1]))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        public void Reverse(ImplementationMethod method = ImplementationMethod.STANDART)
        {
            if (method == ImplementationMethod.STANDART)
            {
                Array.Reverse(array, 0, array.Length);
            }
            else
            {
                for (int i = 0; i < array.Length / 2; i++)
                {
                    int mirrorIndex = array.Length - i - 1;
                    (array[i], array[mirrorIndex]) = (array[mirrorIndex], array[i]);
                }
            }
        }
        public Pair[] CalculateFreq()
        {
            int countLenght = 0;
            Pair[] pairs = new Pair[array.Length];

            //Objects pair are created on first use

            for (int i = 0; i < array.Length; i++)
            {
                bool isElement = false;
                for (int j = 0; j < countLenght; j++)
                {
                    if (array[i] == pairs[j].Number)
                    {
                        pairs[j].Freq++;
                        isElement = true;
                        break;
                    }

                }
                if (!isElement)
                {
                    pairs[countLenght] = new(array[i], 1);
                    countLenght++;
                }
            }

            Array.Resize(ref pairs, countLenght);

            return pairs;
        }
        public Pair[] CalculateSubSequences()
        {

            int maxIndex = -1;
            Pair[] pairs = new Pair[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                if (maxIndex == -1)
                {
                    pairs[++maxIndex] = new(array[i], 1);
                }
                else if (pairs[maxIndex].Number != array[i])
                {
                    pairs[++maxIndex] = new(array[i], 1);
                }
                else
                {
                    pairs[maxIndex].Freq++;
                }
            }

            Array.Resize(ref pairs, maxIndex + 1);

            return pairs;

        }
        public Pair? GetLongestSubSequence()
        {

            Pair[] pairs = CalculateSubSequences();

            if (pairs.Length == 0)
            {
                return null;
            }

            Pair resultPair = pairs[0];

            for (int i = 1; i < pairs.Length; i++)
            {
                if (pairs[i].Freq > resultPair.Freq)
                {
                    resultPair = pairs[i];
                }

            }

            return resultPair;

        }
        public int Sum()
        {
            int sum = 0;
            for(int i = 0; i <= array.Length; i++)
            {
                sum += array[i];

            }
            return sum;
        }


        #endregion
    }
}
