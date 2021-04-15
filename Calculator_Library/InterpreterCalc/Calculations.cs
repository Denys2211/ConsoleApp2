﻿using System;
using Collections;
using Calculator;

namespace InterpreterCalc
{

    delegate NumberExpression Operation(NumberExpression left, NumberExpression right);

    public class Calculations : ICalculator
    {
        private Operation Variabl { get; set; }
        private readonly Operation[] MathOp;
        private NumberExpression[] Number { get; set; }
        public double Result { get; private set; }
        private IContext Context { get; set; }
        public int IndexList { get; private set; }
        internal Calculations(IContext context)
        {
            Context = context;
            MathOp = CreateMasMathOperation();
        }

        private Operation[] CreateMasMathOperation()
        {
            return new Operation[]
                        {
                (left, right) => left + right,
                (left, right) => left - right,
                (left, right) => left / right,
                (left, right) => left * right
                        };
        }

        public void CreateExpression(string input)
        {

            string value = "";
            for (int i = 0; i < input.Length; i++)
            {
                string symbol = input.Substring(i, 1);

                char chr = symbol.ToCharArray()[0];

                if (!char.IsDigit(chr) && chr != '.' && value != "")
                {
                    Context[IndexList].Add(value);

                    value = "";
                }
                if (symbol.Equals("("))
                {
                    IndexList++;

                    i = CalculationInBracket(i, input);
                }

                if (symbol.Equals("+") ||
                    symbol.Equals("-") ||
                    symbol.Equals("*") ||
                    symbol.Equals("/"))

                    Context[IndexList].Add(symbol);

                else if (char.IsDigit(chr) || chr == '.')
                {
                    value += symbol;

                    if (i == (input.Length - 1))
                        Context[IndexList].Add(value);

                }
            }
        }

        public void CalculateExpression()
        {
            for (int i = 1; i < Context[IndexList].Count; ++i)
            {
                if (Context[IndexList][i] == "/")
                {
                    Variabl = MathOp[2];
                    CreateOperations(i);
                    i -= 1;
                }
                if (Context[IndexList][i] == "*")
                {
                    Variabl = MathOp[3];
                    CreateOperations(i);
                    i -= 1;
                }
            }
            for (int i = 1; i < Context[IndexList].Count; ++i)
            {
                if (Context[IndexList][i] == "+")
                {
                    Variabl = MathOp[0];
                    CreateOperations(i);
                    i -= 1;
                }
                if (Context[IndexList][i] == "-")
                {
                    Variabl = MathOp[1];
                    CreateOperations(i);
                    i -= 1;
                }
            }
        }

        public void FilterNumbers()
        {
            Number = new NumberExpression[Context[IndexList].Count];

            for (int i = Context[IndexList].Count - 1; i >= 0; i--)
            {
                if (Context[IndexList][i] != "+" &&
                    Context[IndexList][i] != "-" &&
                    Context[IndexList][i] != "*" &&
                    Context[IndexList][i] != "/")
                {
                    Number[i] = i;
                    Number[i].IndexList = IndexList;
                    Number[i].Context = Context;
                }
            }
        }
        private void CreateOperations(int index)
        {
            
            Operations expression = new Operations(Number[index - 1], Number[index + 1], Variabl);

            Result = expression.Interpret();

            Context.RemoveList(index - 1, IndexList);

            Context.RemoveList(index - 1, IndexList);

            Context[IndexList][index - 1] = Convert.ToString(Result);
            
        }

        private int CalculationInBracket(int i, string input)
        {
            string innerExp = "";
            i++;
            int bracketCount = 0;

            for (; i < input.Length; i++)
            {
               string symbol = input.Substring(i, 1);

                if (symbol.Equals("(")) bracketCount++;

                if (symbol.Equals(")"))
                {
                    if (bracketCount == 0) break;
                    bracketCount--;
                }
                innerExp += symbol;
            }
            CreateExpression(innerExp);

            FilterNumbers();

            CalculateExpression();

            IndexList--;

            Context[IndexList].Add(Result.ToString());

            return i;

        }
    }
}