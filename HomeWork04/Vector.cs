using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_04
{
    internal class Vector
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

        public Vector(Vector arrayIn) : this((arrayIn == null)?0:arrayIn.Lenght)
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
                if (index >= array.Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return array[index];
            }
            set
            {
                if (index >= array.Length)
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
            for(int i = 0; i < indexFill; i++)
            {
                array[i] = ListNumbers[i];
            }
            for (int i = indexFill; i < array.Length; i++)
            {
                array[i] = 0;
            }
        }
        #endregion

        #region sort_methods
        public void SortBobble(SortingDirection direct = SortingDirection.ASC)
        {
            if (array.Length < 2)
            {
                return;
            }
            
            for (int i = 0; i < array.Length - 1; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (((array[j] > array[j + 1]) && (direct == SortingDirection.ASC))
                        || ((array[j] < array[j + 1]) && (direct == SortingDirection.DESC)))
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
                    int index = (direct == SortingDirection.ASC)? k : (temp.Length - k - 1);
                    array[index] = i + minValue;
                    k++;
                }
            {

            }
        }

        public void SortQuick(SortingDirection direct = SortingDirection.ASC, TypeQuickSort typeQS = TypeQuickSort.CENTRUM)
        {
            if (array.Length < 2)
            {
                return;
            }

            quicksort(0, array.Length-1, direct, typeQS);
            return;

            void quicksort(int low, int high, SortingDirection direct, TypeQuickSort typeQS)
            {
                if (low < high)
                {
                    int p = partition(low, high, direct, typeQS);
                    if (typeQS == TypeQuickSort.RIGHT)
                    {
                        quicksort(low, p - 1, direct, typeQS);
                        quicksort(p, high, direct, typeQS);
                    }
                    else
                    {
                        quicksort(low, p, direct, typeQS);
                        quicksort(p + 1, high, direct, typeQS);
                    }
                }
            }

            int partition(int low, int high, SortingDirection direct, TypeQuickSort typeQS)
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
                    if (direct == SortingDirection.ASC)
                    {
                        while (array[i] < pivot) i++;
                        while (array[j] > pivot) j--;
                    }
                    else
                    {
                        while (array[i] > pivot) i++;
                        while (array[j] < pivot) j--;
                    }

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
        #endregion

        #region other_methods
        public bool IsPalindrome()
        {
            bool result = true;
            for (int i = 0; i < array.Length/2 ; i++)
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
        #endregion
    }
}
