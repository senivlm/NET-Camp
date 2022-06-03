using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_06_1
{
    public class RowDataApartment : RowData
    {
        //#region static_fields
        //private readonly static Storage<RowDataApartment> storage = new();
        //#endregion

        #region fields
        private int number;
        #endregion

        #region properties
        public int Number { get => this.number; }
        #endregion

        #region constructors
        public RowDataApartment() : base() { } //implementation in abstract class
        public RowDataApartment(string str) : base(str) { } //implementation in abstract class
        #endregion

        #region overrided_methods
        private protected override void Clean()
        {
            this.number = -1;
        }

        public override void FillingFromString(string str)
        {
            if (!Int32.TryParse(str, out this.number))
            {
                //The integrity of the data, if not read, then cleaned
                Clean();
            }
        }

        public override string? ToString()
        {
            return $"Apartment #{this.number}";
        }

        public override bool IsEmpry()
        {
            return (this.number < 0); 
        }

        public override bool EqualsKey(RowData obj)
        {
            if (obj is RowDataApartment)
            {
                //Validate only for key fields
                RowDataApartment tmpObj = (RowDataApartment)obj;
                if (this.Number == tmpObj.Number)
                {
                    return true;
                }
            }
            return false;
        }

        public override void Update(RowData obj)
        {
            if (obj is RowDataApartment)
            {
                RowDataApartment tmpObj = (RowDataApartment)obj;
                this.number = tmpObj.number;
            }
        }
        #endregion
    }
}
