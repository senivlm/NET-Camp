using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_06_1
{
    public class RowDataOwner : RowData
    {
        //#region static_fields
        //private readonly static Storage<RowDataOwner> storage = new();
        //#endregion

        #region fields
        private string name = "";
        #endregion

        #region properties
        public string Name{ get => this.name;}
        #endregion

        #region constructors
        public RowDataOwner() : base() { } //implementation in abstract class
        public RowDataOwner(string str) : base(str) { } //implementation in abstract class
        #endregion

        #region overrided_methods
        private protected override void Clean()
        {
            this.name = "";
        }
        public override void FillingFromString(string str)
        {
            this.name = str;
        }
        public override string? ToString()
        {
            return $"Owner #{this.name}";
        }
        public override bool IsEmpry()
        {
            return (this.name.Equals(""));
        }
        public override bool EqualsKey(RowData obj)
        {
            if (obj is RowDataOwner)
            {
                //Validate only for key fields
                RowDataOwner tmpObj = (RowDataOwner)obj;
                if (this.Name.Equals(tmpObj.Name))
                {
                    return true;
                }
            }
            return false;
        }
        public override void Update(RowData obj)
        {
            if (obj is RowDataOwner)
            {
                RowDataOwner tmpObj = (RowDataOwner)obj;
                this.name = tmpObj.name;
            }
        }
        #endregion
    }
}
