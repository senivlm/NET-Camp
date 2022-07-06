using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_12_3
{
    public class Calculator
    {
        private static Dictionary<char, Operation> operationPriority = new() {
        {'(', new(0)},
        {'+', new(1, (x,y)=>(x+y))},
        {'-', new(1, (x,y)=>(x-y))},
        {'*', new(2, (x,y)=>(x*y))},
        {'/', new(2, (x,y)=>(x/y))},
        {'^', new(3, (x,y)=>Math.Pow(x, y))},

        //функції
        {'a', new(99, (x,y)=>Math.Cos(y), true, "cos")},
        {'b', new(99, (x,y)=>Math.Sin(y), true, "sin")},
        {'c', new(99, (x,y)=>Math.Tan(y), true, "tan")},

        {'~', new(99, (x,y)=>(-y), true, "")}	//	Унарный мінус
        };

        private string inputString;
        private readonly List<string> outputList;

        public string InputString
        {
            get => inputString;
            set
            {
                inputString = value;
                outputList.Clear();
            }
        }
        public string OutputString => GetPolandFormula();
        public double Result => Calc();
        
        #region events
        public event Action<string>? NotifyStep;
        #endregion

        public Calculator() : this("") { } 
        public Calculator(string inputString)
        {
            this.inputString = inputString;
            this.outputList = new();
        }

        public string Trasform(string inputString)
        {
            InputString = inputString; //Важливо InputString, а не inputString щоб відпрацював Set 
            return Trasform();
        }
        public string Trasform()
        {
            outputList.Clear();

            Stack<char> stack = new();

            bool addStar = false;
            for (int i = 0; i < inputString.Length; i++)
            {

                char currentChar = inputString[i];
                char previusChar = (i==0) ? ' ' : inputString[i-1];

                if (currentChar == ' ')
                {
                    continue;
                }

                //підміна 1
                // 324( пертворюемо на 324*(
                if (currentChar == '(' && Char.IsDigit(previusChar) && !addStar)
                {
                    addStar = true;
                    currentChar = '*';
                    i--;
                }

                //підміна 2
                //Функції замінюемо на літери
                if (Char.IsLetter(currentChar))
                {
                    string func = GetFunction(inputString, ref i);
                    var key = operationPriority.Where(p => (p.Value.nameFunc == func)).First();//по умові формула прийшла без помилок, тому знайде обовє'язково
                    currentChar = key.Key;
                }


                //обробка currentChar
                if (currentChar == '(')
                {
                    //	Скобку у стек
                    stack.Push(currentChar);
                    addStar = false;
                }
                else if (currentChar == ')')
                {
                    //	Все зі стеку до ( у результат
                    while (stack.Count > 0 && stack.Peek() != '(')
                        outputList.Add(stack.Pop().ToString());
                    //	Видаляємо скобку
                    stack.Pop();
                }
                else if(Char.IsDigit(currentChar))
                {
                    //	Число у результат
                    outputList.Add(GetNumber(inputString, ref i));
                }
                //	оператор
                else if (operationPriority.ContainsKey(currentChar))
                {
                    char op = currentChar;
                    //	Унарный минус
                    if (op == '-' && (i == 0 || (i > 1 && operationPriority.ContainsKey(previusChar))))
                        //	замінюємо на тільду, щоб відрізнити
                        op = '~'; 

                    //	Виймаємо усе більш приорітетне
                    while (stack.Count > 0 && (operationPriority[stack.Peek()].priority >= operationPriority[op].priority))
                        outputList.Add(stack.Pop().ToString());

                    //додаємо оператор у стек
                    stack.Push(op);
                }
                else
                {
                    throw new ArgumentException("Unknown operation " + currentChar);
                }
            }
            //	усе зі стеку до результату
            foreach (char op in stack)
                outputList.Add(op.ToString());

            return GetPolandFormula();

        }

        public double Calc(string inputString)
        {
            InputString = inputString; //Важливо InputString, а не inputString щоб відпрацював Set
            return Calc();
        }
        public double Calc()
        {
            Trasform();

            Stack<double> locals = new();
            int counter = 0;

            for (int i = 0; i < outputList.Count; i++)
            {
                string currentElement = outputList[i];
                char firstChar = currentElement[0];

                //Число
                if (Char.IsDigit(firstChar))
                {
                    locals.Push(Double.Parse(currentElement.Replace(".", ",")));
                }
                //оператор
                else if (operationPriority.ContainsKey(firstChar))
                {
                    Operation currentOp = operationPriority[firstChar];
                    counter += 1;


                    //	унарний ?
                    if (currentOp.isUno)//(firstChar == '~')
                    {
                        //беремо число для оператора
                        double last = locals.Count > 0 ? locals.Pop() : 0;
                        locals.Push(currentOp.formula(0, last));

                        if (currentOp.nameFunc == "")
                        {
                            NotifyStep?.Invoke($"step {counter}) {firstChar}{last} = {locals.Peek()}");
                        }
                        else
                        {
                            NotifyStep?.Invoke($"step {counter}) {currentOp.nameFunc}({last}) = {locals.Peek()}");
                        }

                    }
                    else {

                        double second = locals.Count > 0 ? locals.Pop() : 0;
                        double first = locals.Count > 0 ? locals.Pop() : 0;

                        locals.Push(currentOp.formula(first, second));

                        if (currentOp.nameFunc == "")
                        {
                            NotifyStep?.Invoke($"step {counter}) {first} {firstChar} {second} = {locals.Peek()}");
                        }
                        else
                        {
                            NotifyStep?.Invoke($"step {counter}) {currentOp.nameFunc} ({first} ,{second}) = {locals.Peek()}");
                        }
                    }
                }
                else
                {
                    throw new ArgumentException("Unknown operation " + currentElement);
                }
            }

            return locals.Pop();
        }

        private string GetPolandFormula()
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in outputList)
            {
                char firstChar = s[0];
                if (operationPriority.ContainsKey(firstChar) && operationPriority[firstChar].nameFunc != "")
                {
                    sb.Append(operationPriority[firstChar].nameFunc + " ");
                }
                else
                {
                    sb.Append(s + " ");
                } 
            }
            return sb.ToString();
        }
        private string GetNumber(string expr, ref int pos)
        {

            string strNumber = "";
            for (; pos < expr.Length; pos++)
            {
                char num = expr[pos];
                if (Char.IsDigit(num) || num=='.' || num==',')
                    strNumber += num;
                else
                {
                    pos--;
                    break;
                }
            }
            return strNumber;
        }
        private string GetFunction(string expr, ref int pos)
        {

            string strFunction = "";
            for (; pos < expr.Length; pos++)
            {
                char num = expr[pos];
                if (Char.IsLetter(num))
                    strFunction += num;
                else
                {
                    pos--;
                    break;
                }
            }
            return strFunction;
        }

    }
}
