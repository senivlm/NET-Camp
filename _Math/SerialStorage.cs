using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math
{
    public interface ISerialStorage
    {
        #region methods
        public void Add(int nom);
        public void ExportToArray(int[] extArray, int indexStart1);
        #endregion
    }
}
