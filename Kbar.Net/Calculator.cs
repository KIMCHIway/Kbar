using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kbar.Net
{
    class Calculator
    {
        public string clac(string formula)
        {
            bool isNum = false;

            for (int i = 0; i < formula.Length; i++)
            {
                string s = formula[i].ToString();

                if (isOper(s))
                {
                    if ( s != "(" && s != ")")
                    {
                        if(isNum == false) return "Syntax Error";
                        isNum = false;
                    }

                    if (stackOper.Count == 0)
                    {
                        stackOper.Push(s);
                    }
                    else
                    {
                        if (s == "(") stackOper.Push(s);
                        else if (s == ")")
                        {
                            // ( 올때까지 계산처리함. ) 
                            while (stackOper.Count > 0)
                            {
                                string oper = stackOper.Pop();
                                if ("(".Equals(oper)) break;
                                double val = calc(stackValue.Pop(), stackValue.Pop(), oper);
                                stackValue.Push(val);
                            }
                        }
                        else
                        {
                            // 비교해서, 기존의 operator가 우선순위가 높은 경우, 계산함.  
                            int old_priority = get_oper_priority(stackOper.Peek());
                            int curr_priority = get_oper_priority(s);
                            if (old_priority >= curr_priority)
                            {
                                // 계산 
                                double val1 = stackValue.Pop();
                                double val2 = stackValue.Pop();
                                double val = calc(val1, val2, stackOper.Pop());
                                stackValue.Push(val);
                                stackOper.Push(s);
                            }
                            else
                            {
                                stackOper.Push(s);
                            }
                        }
                    }
                }
                else
                {
                    if (isNum == true) return "Syntax Error";
                    isNum = true;

                    stackValue.Push(Convert.ToDouble(s));
                }
            }

            while (stackOper.Count > 0)
            {
                double val = calc(stackValue.Pop(), stackValue.Pop(), stackOper.Pop());
                stackValue.Push(val);
            }

            double value = stackValue.Pop();

            return value.ToString();
        }

        private double calc(double val1, double val2, string oper)
        {
            if (oper == "+") return val1 + val2;
            if (oper == "-") return val2 - val1;
            if (oper == "*") return val1 * val2;
            if (oper == "/") return val2 / val1;
            throw new Exception("unknown oper : " + oper);
        }

        private int get_oper_priority(string oper)
        {
            if (oper == "(" || oper == ")") return 0;
            if (oper == "-" || oper == "+") return 1;
            if (oper == "*" || oper == "/") return 2;
            throw new Exception("unknown oper " + oper);
        }

        private bool isOper(string s)
        {
            if (s == "(" ||
                s == ")" ||
                s == "+" ||
                s == "-" ||
                s == "*" ||
                s == "/") return true;
            return false;
        }


        private Stack<string> stackOper = new Stack<string>();
        private Stack<double> stackValue = new Stack<double>();
    }
}
