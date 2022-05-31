using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math
{
    public class SerialStorageArray : ISerialStorage
    {
        #region fields
        private readonly int[] array;
        private int indexArray;
        #endregion

        #region constructors
        public SerialStorageArray(int n)
        {
            this.array = new int[n];
            this.indexArray = 0;

        }
        #endregion

        #region methods
        public void Add(int nom)
        {

            if (indexArray >= array.Length)
            {
                throw new ArgumentException($"Storage is full. indexArray = {indexArray}");
            }
            array[indexArray++] = nom;
        }
        public void ExportToArray(int[] extArray, int indexStart1)
        {
            for (int i = 0; i < array.Length; i++)
            {
                extArray[indexStart1 + i] = array[i];
            }
        }
        #endregion
    }
}
