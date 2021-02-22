﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    // нетерминальное выражение для вычитания
    class SubtractExpression : IExpression
    {
        IExpression leftExpression;
        IExpression rightExpression;

        public SubtractExpression(IExpression left, IExpression right)
        {
            leftExpression = left;
            rightExpression = right;
        }

        public double Interpret(IContext context)
        {
            return leftExpression.Interpret(context) - rightExpression.Interpret(context);
        }
    }
}
