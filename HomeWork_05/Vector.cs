using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_05
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

        #region indexes
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
        #endregion

        #region init_methods
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
        public void SortSplitMerge(SortingDirection direct)
        {
            if (array.Length < 2)
            {
                return;
            }

            SortSplitMergeInternal(0, array.Length - 1, direct);
            return;
// Краще явно вказувати специфікатор доступу
            void SortSplitMergeInternal(int indexStart, int indexFinish, SortingDirection direct)
            {

                if (indexFinish <= indexStart)
                {
                    return;
                }

                int indexMidle = (indexStart + indexFinish) / 2;
                SortSplitMergeInternal(indexStart, indexMidle, direct);
                SortSplitMergeInternal(indexMidle + 1, indexFinish, direct);
                Merge(indexStart, indexMidle, indexFinish, direct);

            }

            void Merge(int indexStart1, int indexFinish1, int indexFinish2, SortingDirection direct)
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
                    if ((array[i] <= array[j] && direct == SortingDirection.ASC)
                        || (array[i] >= array[j] && direct == SortingDirection.DESC))
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
        public void SortHeap(SortingDirection direct)
        {

            if (array.Length < 2)
            {
                return;
            }
            
            //Create full binary tree
            for (int i = array.Length / 2 - 1; i >= 0; i--)
            {
                UpdateTreeFromParent(array.Length, i, direct);
            }

            //Sort array
            for (int i = array.Length - 1; i >= 0; i--)
            {
                (array[0], array[i]) = (array[i], array[0]);
                UpdateTreeFromParent(i, 0, direct);
            }
            return;
        

            void UpdateTreeFromParent(int sizeTree, int indexParent, SortingDirection direct)
            {
                NotifyStep?.Invoke($"{this.ToString()} n={sizeTree} i={indexParent}");

                int indexTarget = indexParent;

                int indexChildL = 2 * indexParent + 1; // left = 2*i + 1
                int indexChildR = 2 * indexParent + 2; // right = 2*i + 2


                if (indexChildL < sizeTree 
                    && (direct == SortingDirection.ASC && array[indexChildL] > array[indexTarget]
                        || direct == SortingDirection.DESC && array[indexChildL] < array[indexTarget])
                    )
                {
                    indexTarget = indexChildL; 
                }

                if (indexChildR < sizeTree
                    && (direct == SortingDirection.ASC && array[indexChildR] > array[indexTarget]
                        || direct == SortingDirection.DESC && array[indexChildR] < array[indexTarget])
                    )
                {
                    indexTarget = indexChildR;
                }

                if (indexTarget != indexParent)
                {
                    (array[indexTarget], array[indexParent]) = (array[indexParent], array[indexTarget]);
                    UpdateTreeFromParent(sizeTree, indexTarget, direct);
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
        public bool IsSorted(SortingDirection direct)
        {// Елегантніше через передачу компаратора
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
#endregion
    }
}
