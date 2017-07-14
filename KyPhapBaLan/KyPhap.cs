using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KyPhapBaLan
{
    class KyPhap
    {
        public string num { get; set; }

        private static int GetPriority(string ope)
        {
            if (ope == "!") return 4;
            else if (ope == "^") return 3;
            else if (ope == "*" || ope == "/" || ope == "x" || ope == ":" || ope == "%") return 2;
            else if (ope == "+" || ope == "-") return 1;
            else return 0;
        }

        private static int IsOperator(string ope)
        {
            /**
             *  0 : chu so
             *  1 : dau ngoac
             *  2 : dau
             **/
            if (GetPriority(ope) == 0)
            {
                if (ope != "(" && ope != ")")
                    return 0;
                else return 1;
            }
            return 2;
        }

        private static void Normalization(ref string exp)
        {
            exp = exp.Trim();
            while (exp.IndexOf(" ") > 0)
                exp = exp.Replace(" ", "");
            
            StringBuilder s = new StringBuilder(exp);

            for (int i = 0; i < s.Length - 1; i++)
                if (IsOperator(s[i].ToString()) == 0 && s[i + 1] == '(')
                    s.Insert(i + 1, "*");

            for (int i = 0; i < s.Length - 1; i++)
                if (s[i] != ' ')
                    if (IsOperator(s[i].ToString()) != IsOperator(s[i + 1].ToString()))
                        s.Insert(i + 1, ' ');

            exp = s.ToString();
        }

        private static string Pop(ref List<string> exp)
        {
            int length = exp.Count;
            string pop = exp[length - 1];

            exp.RemoveAt(length - 1);

            return pop;
        }

        public static List<string> ConvertToPostfix(string exp)
        {
            List<string> stack = new List<string>();
            List<string> output = new List<string>();

            Normalization(ref exp);

            string[] expSpl = exp.Split(' ');

            foreach (string i in expSpl)
            {
                if (IsOperator(i) == 0)
                    output.Add(i);
                else if (IsOperator(i) == 1)
                {
                    if (i == "(")
                        stack.Add(i);
                    else
                    {
                        string pop = Pop(ref stack);

                        while (pop != "(")
                        {
                            output.Add(pop);
                            pop = Pop(ref stack);
                        }
                        
                    }

                }
                else
                {
                    while (stack.Count > 0 && GetPriority(stack[stack.Count - 1]) >= GetPriority(i))
                        output.Add(Pop(ref stack));
                    stack.Add(i);
                }
            }
            while (stack.Count > 0)
                output.Add(Pop(ref stack));

            return output;
        }

        public static SoNguyenLon Calc(List<string> input)
        {
            List<string> stack = new List<string>();
            for (int i = 0; i < input.Count; i++)
            {
                if (IsOperator(input[i]) == 0)
                    stack.Add(input[i]);
                else
                {
                    SoNguyenLon b = Pop(ref stack);
                    SoNguyenLon a = Pop(ref stack);
                    SoNguyenLon c = "";
                    if (input[i] == "+") c = a + b;
                    else if (input[i] == "-") c = a - b;
                    else if (input[i] == "*" || input[i] == "x") c = a * b;
                    else if (input[i] == "/" || input[i] == ":") c = a / b;
                    else if (input[i] == "^") c = a ^ b;
                    else if (input[i] == "%") c = a % b;
                    else if (input[i] == "!")
                    {
                        stack.Add(a.ToString());
                        c = SoNguyenLon.GiaiThua(b.ToInt());
                    }
                    stack.Add(c.ToString());
                }
            }
            return Pop(ref stack);
        }
    }   
}
