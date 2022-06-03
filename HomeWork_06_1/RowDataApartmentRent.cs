using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_06_1
{
    public class RowDataApartmentRent : RowData
    {
        //#region static_fields
        //private readonly static Storage<RowDataApartmentRent> storage = new();
        //#endregion

        #region properties
        public DateTime Period { get; set; }
        public RowDataApartment? Apartment { get; set; }
        public RowDataOwner? Owner { get; set; }
        #endregion

        #region constructors
        public RowDataApartmentRent() : base() { } //implementation in abstract class
        public RowDataApartmentRent(string str) : base(str) { } //implementation in abstract class
        #endregion

        #region overrided_methods
        private protected override void Clean()
        {
            this.Period = default;
            this.Apartment = default;
            this.Owner = default;
        }

        public override void FillingFromString(string str)
        {
            bool ReadingSuccessFully = true;

            string[] strArray = str.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != 3)
            {
                ReadingSuccessFully = false;
            }

            if (ReadingSuccessFully)
            {
                DateTime tmpPeriod;
                ReadingSuccessFully = DateTime.TryParse(strArray[0], out tmpPeriod);
                if (ReadingSuccessFully)
                {
                    this.Period = tmpPeriod;
                }
            }

            if (ReadingSuccessFully)
            {
                RowDataApartment tmpApartment = new(strArray[1]);
                if (!tmpApartment.IsEmpry())
                {
                    this.Apartment = tmpApartment;
                }
                else
                {
                    ReadingSuccessFully = false;
                }
            }

            if (ReadingSuccessFully)
            {
                RowDataOwner tmpOwner = new(strArray[2]);
                if (!tmpOwner.IsEmpry())
                {
                    this.Owner = tmpOwner;
                }
                else
                {
                    ReadingSuccessFully = false;
                }
            }

            if (!ReadingSuccessFully)
            {
                Clean();
            }
        }

        public override string? ToString()
        {
            return $"Apartment rent Period {Period} {this.Apartment?.ToString()} {this.Owner?.ToString()}";
        }

        public override bool IsEmpry()
        {
            return (this.Period == default && this.Apartment == default && this.Owner == default);
        }

        public override bool EqualsKey(RowData obj)
        {
            if (obj  is RowDataApartmentRent)
            {
                //Validate only for key fields
                RowDataApartmentRent tmpObj = (RowDataApartmentRent)obj;
                if (this.Period == tmpObj.Period
                    || this.Apartment.EqualsKey(tmpObj.Apartment))
                {
                    return true;
                }
            }
            return false;
        }

        public override void Update(RowData obj)
        {
            if (obj is RowDataApartmentRent)
            {
                RowDataApartmentRent tmpObj = (RowDataApartmentRent)obj;
                this.Period = tmpObj.Period;
                this.Apartment = tmpObj.Apartment;
                this.Owner = tmpObj.Owner;
            }
        }
        #endregion
    }
}
