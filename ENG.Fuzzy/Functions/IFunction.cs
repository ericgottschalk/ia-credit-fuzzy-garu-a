using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.Fuzzy.Functions
{
    public interface IFunction
    {
        level Level { get; }

        float Begin { get; }

        float End { get; }

        float Fuzzy { get; set; }

        float Value(float x);
    }
}
