using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_06_1
{
    public class RowDataMeterValue : RowData
    {
        #region properties
        public DateTime Period { get; set; }
        public RowDataApartment? Apartment { get; set; }
        public int Value { get; set; }
        #endregion

        #region constructors
        public RowDataMeterValue() : base() { } //implementation in abstract class
        public RowDataMeterValue(string str) : base(str) { } //implementation in abstract class
        #endregion

        #region overrided_methods
        private protected override void Clean()
        {
            this.Period = default;
            this.Apartment = default;
            this.Value = -1;
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
                int tmpValue;
                ReadingSuccessFully = int.TryParse(strArray[2], out tmpValue);
                if (ReadingSuccessFully)
                {
                    this.Value = tmpValue;
                }
            }

            if (!ReadingSuccessFully)
            {
                Clean();
            }
        }

        public override string? ToString()
        {
            return $"Meter value Period {Period} {this.Apartment?.ToString()} Value {this.Value}";
        }

        public override bool IsEmpry()
        {
            return (this.Period == default && this.Apartment == default && this.Value == -1);
        }

        public override bool EqualsKey(RowData obj)
        {
            if (obj is RowDataMeterValue)
            {
                //Validate only for key fields
                RowDataMeterValue tmpObj = (RowDataMeterValue)obj;
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
            if (obj is RowDataMeterValue)
            {
                RowDataMeterValue tmpObj = (RowDataMeterValue)obj;
                this.Period = tmpObj.Period;
                this.Apartment = tmpObj.Apartment;
                this.Value = tmpObj.Value;
            }
        }
        #endregion
    }
}
