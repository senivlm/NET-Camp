using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_06_1
{//Назва класу мені  не подобається
    public abstract class RowData
    {
        //TODO Since the static field is not inherited, then change it to a dictionary with the key Object Type. 
        private protected static Storage<RowData> storage = new();

        public RowData() 
        {//завдання  конструктора - створювати. Він перший, тому немає що очищати.
            Clean();
        }
        public RowData(string str) : this()
        {
            FillingFromString(str);
            if (!IsEmpry())
            {
                Save();
            }
        }

        #region abstract_metods
        private protected abstract void Clean();
        public abstract void FillingFromString(string str);
        public override abstract string? ToString();
        public abstract bool IsEmpry();
        public abstract bool EqualsKey(RowData obj); //To check only on key fields
        public abstract void Update(RowData obj);
        #endregion

        #region implementation_metods
        public static void ShowStorage()
        {
            Console.Write(storage);
        }
        public void Save()
        {
            storage.Save(this);
        }
        #endregion

    }
}
