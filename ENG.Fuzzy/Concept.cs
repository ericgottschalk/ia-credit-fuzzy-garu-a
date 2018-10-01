using ENG.Fuzzy.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ENG.Fuzzy
{
    public class Concept
    {
        public float Value { get; set; }

        public List<IFunction> Functions { get; }

        public Concept(float value = 0)
        {
            this.Value = value;
            this.Functions = new List<IFunction>();
        }

        public float Fuzzify(level level)
        {
            var function = this.Functions.FirstOrDefault(t => t.Level == level);

            return function.Value(this.Value);
        }

        public float Defuzzify(float precision)
        {
            var begin = this.Functions.Min(t => t.Begin);
            var end = this.Functions.Max(t => t.End);
            var area = -0.5f;
            var sum = 0f;

            for (var i = begin; i <= end; i += precision)
            {
                var value = this.Functions.Select(t => Math.Min(t.Value(i), t.Fuzzy)).Max();

                area += (value * i);
                sum += value;
            }

            if (sum == 0)
            {
                return 0f;
            }

            return area / sum;
        }

        public float GetValue(level level)
        {
            var function = this.Functions.FirstOrDefault(t => t.Level == level);

            return function.Fuzzy;
        }
    }
}
