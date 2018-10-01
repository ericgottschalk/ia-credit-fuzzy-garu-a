using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.Fuzzy
{
    public static class Operator
    {
        public static float Not(float value) => 1 - value;

        public static float Or(float left, float right) => Math.Max(left, right);

        public static float And(float left, float right) => Math.Min(left, right);
    }
}
