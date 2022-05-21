using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_03
{
    internal class Vector
    {
        private readonly int[] array;

        public int Lenght => array.Length;

        public Vector(uint n)
        {
            if (n == 0)
            {
                throw new ArgumentException("Vector size is 0");
            }
            array = new int[n];
        }

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
            Random ran = new();
            for (int i = 0; i < array.Length; i++)
            {
                while (true)
                {
                    int nom = ran.Next(1, array.Length + 1);
                    bool isExist = false;
                    for (int j = 0; j < i; j++)
                    {
                        if (array[j] == nom)
                        {
                            isExist = true;
                            break;
                        }
                    }
                    if (!isExist)
                    {
                        array[i] = nom;
                        break;
                    }
                }

            }
        }

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

        public void Reverse(ImplementationMethod method = ImplementationMethod.standart)
        {
            if (method == ImplementationMethod.standart)
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

            int countLenght = 0;
            Pair[] pairs = new Pair[array.Length];

            for (int i = 0; i < array.Length; i++)
            {
                if (pairs[countLenght] == null)
                {
                    pairs[countLenght] = new(array[i], 1);
                }
                else if (pairs[countLenght].Number != array[i])
                {
                    pairs[++countLenght] = new(array[i], 1);
                }
                else
                {
                    pairs[countLenght].Freq++;
                }
            }

            Array.Resize(ref pairs, countLenght);

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

        public override string? ToString()
        {
            string str = "";
            for (int i = 0; i < array.Length; i++)
            {
                str += array[i] + " ";
            }

            return str;
        }

        public override bool Equals(object? obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
