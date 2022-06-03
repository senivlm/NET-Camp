using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_06_1
{
    public class Storage<T> where T : RowData//, new()
    {
        #region fields
        private readonly List<T> list;
        #endregion

        #region constructors
        public Storage()
        {
            this.list = new List<T>();
        }
        #endregion
   
        public void Save(T obj)
        {
            //If obj exist then update else add
            T? findingObj = Find(obj);
            if (findingObj != null)
            {
                findingObj?.Update(obj);
            }
            else
            {
                list.Add(obj);
            }
        }
        public bool Exist(T obj)
        {
            T? tmpObj = Find(obj);
            return (tmpObj != null);
        }
        public void Remove(T obj)
        {
            T? tmpObj = Find(obj);
            if (tmpObj != null)
            {
                list.Remove(tmpObj);
            }
        }
        public T? Find(T obj)
        {
            for (int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i].EqualsKey(obj)) //search by key
                {
                    return list[i];
                }
            }
            return null;
        }

        public override string? ToString()
        {
            var sb = new System.Text.StringBuilder();
            foreach (T str in list)
            {
                sb.Append($"{str.ToString()}\n");
            }
            return sb.ToString();
        }
    }
}
