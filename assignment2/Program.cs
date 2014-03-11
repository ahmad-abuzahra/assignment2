using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Data;
using System.Collections;

namespace assignment2
{
    
    class Program
    {

        static public int Position = 0;


        // Main Function //
        static void Main(string[] args)
        {
            OutputLine();
        }

        
        //  loop until quit // 
        static void OutputLine()
        {
            Console.WriteLine("Enter your expression or enter quit to exit");
            Console.WriteLine();
            string input = Console.ReadLine();
            if (input.ToLower()  != "quit")
            {
                do
                {
                    checkQutation(input);
                    input = Console.ReadLine();
                } while (input.ToLower() != "quit");
            }
           
        }

        
        // Check Alph and Operation character //
        static void checkQutation(string input)
        {
                       
            Boolean ErrorFlag = true;
            for (int i = 0; i < input.Length; i++)
            {
                if (Char.IsLetter(input[i]) )
                {
                    Console.WriteLine("Can’t parse the expression . Error position: " + (i+1) );
                    ErrorFlag = false;
                    break ; 

                }
                else
                {
                    if (Char.IsNumber(input[i]) || Char.IsWhiteSpace(input[i]) || input[i] == '.')
                    {

                        
                    }
                    else
                    {
                        if ((input[i] != '+' && input[i] != '-' && input[i] != '*' && input[i] != '/' && input[i] != '(' && input[i] != ')'))
                        {
                            Console.WriteLine("Can’t parse the expression . Error position: "  + (i + 1));
                            ErrorFlag = false;
                            break;
                        }
                    }
                    

                }

                
            }
            
            string Result = "";
            Result = CheckParentheses(input);
            if (Result.Length > 0 )
            {
                Console.WriteLine(Result);
                ErrorFlag = false;
                
            }

            if (ErrorFlag == true)
            {
                Result = Calculate(input);
               Console.WriteLine(Result);
            }
        }

        // Check Parentheses //
        public static string CheckParentheses(string input)
        {
            int LeftParentheses = 0;
            int RightParentheses = 0;
            string FinalResult = "";
            for (int i = 0; i < input.Length; i++)
            {
                if ((input[i] == '('))
                {
                    LeftParentheses++;
                }
                if ((input[i] == ')'))
                {
                    RightParentheses++;
                }
            }

            if ((LeftParentheses != RightParentheses) || ((LeftParentheses + RightParentheses) == input.Length))
            {
                for (int j = 0; j < input.Length; j++)
                {
                    if ((input[j] == '(') || (input[j] == ')'))
                    {
                        FinalResult = "Can’t parse the expression . Error position: " + (j + 1);
                        break;
                    }
                }
            }
            return FinalResult;
        }

        // Evaluation the  expression //
        public static string Calculate(String input)
        {
            // for error Index  //
            int position = 0;

            // for Print Final Result //
            string FinalResult = "";

            // Creates and initializes a new Stack to fill charcter
            Stack<String> stack = new Stack<String>();

            try
            {
                string value = "";
                for (int i = 0; i < input.Length; i++)
            {
                
                position = i + 1;
                String s = input.Substring(i, 1);
                char chr = s.ToCharArray()[0];

                if (!char.IsDigit(chr) && chr != '.' && value != "")
                {
                    stack.Push(value);
                    value = "";
                }

                // to calculate Between parentheses //
                if (s.Equals("("))
                {
                    string innerExpretion = "";
                    //Next Character
                    i++;
                    int bracketCount = 0;
                    for (; i < input.Length; i++)
                    {
                        s = input.Substring(i, 1);

                        if (s.Equals("("))
                            bracketCount++;

                        if (s.Equals(")"))
                            if (bracketCount == 0)
                                break;
                            else
                                bracketCount--;


                        innerExpretion += s;
                    }

                    // Recercive Function //
                    stack.Push(Calculate(innerExpretion).ToString());

                }
                else if (s.Equals("+"))
                    stack.Push(s);
                else if (s.Equals("-"))
                    stack.Push(s);
                else if (s.Equals("*"))
                    stack.Push(s);
                else if (s.Equals("/"))
                    stack.Push(s);
                else if (s.Equals(")"))
                {

                }
                else if (char.IsDigit(chr) || chr == '.')
                {
                    value = value + s;

                    if (i == (input.Length - 1))
                        stack.Push(value);

                }
                
            }



            double result = 0;

            // check the stack if more than 3 values to calculate //
            while (stack.Count >= 3)
            {

                double right = Convert.ToDouble(stack.Pop());
                string operators = stack.Pop();
                double left = Convert.ToDouble(stack.Pop());

                if (operators == "+")
                    result = left + right;
                else if (operators == "+")
                    result = left + right;
                else if (operators == "-")
                    result = left - right;
                else if (operators == "*")
                    result = left * right;
                else if (operators == "/")
                    result = left / right;

                stack.Push(result.ToString());
            }
            }
            catch (Exception ex)
            {
                FinalResult = "Can’t parse the expression . Error position: " + position;
            }
            try
            {
                if (FinalResult.Length == 0)
                {
                    double m = Convert.ToDouble(stack.Pop());
                    FinalResult = m.ToString();
                }
                return FinalResult;
            }
            catch (Exception ex)
            {
                return "Can’t parse the expression . Error position: " + position;
            }
           
        }


        


    }
}
