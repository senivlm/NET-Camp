using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_12_3
{
    public struct Operation
    {
        public int priority;
        public Func<double, double, double> formula;
        public bool isUno;
        public string nameFunc;
       
        public Operation() : this(0, (x,y)=>0, false, "") {}
        public Operation(int priority) : this()
        {
            this.priority = priority;
        }
        public Operation(int priority, Func<double, double, double>? formula) : this()
        {
            this.priority = priority;
            this.formula = formula;
        }
        public Operation(int priority, Func<double, double, double> formula, bool isUno, string nameFunc)
        {
            this.priority = priority;
            this.formula = formula;
            this.isUno = isUno;
            this.nameFunc = nameFunc;
        }
    }
}

    
