﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    interface ILogger
    {
        void WritteFile(string text);

        string ReaderFile();
    }
}
