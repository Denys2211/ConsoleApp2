﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    // терминальное выражение
    class NumberExpression : IExpression
    {
        int index;
        public NumberExpression(int variableName)
        {
            index = variableName;
        }
        public double Interpret(IContext context)
        {
            return context.GetList(index);
        }
    }
}
