using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.Fuzzy.Functions
{
    public abstract class Constructor : IFunction
    {
        public level Level { get; protected set; }

        public float Begin { get; protected set; }

        public float End { get; protected set; }

        public float Fuzzy { get; set; }

        public float PointA { get; protected set; }

        public float PointB { get; protected set; }

        public float PointC { get; protected set; }

        public float PointD { get; protected set; }

        public Constructor(level level)
        {
            this.Level = level;
        }

        public virtual float Value(float x)
            => Math.Max(Math.Min(Math.Min((x - this.PointA) / ((this.PointB - this.PointA) == 0 ? 1 : this.PointB - this.PointA), 1), (this.PointD - x) / ((this.PointD - this.PointC) == 0 ? 1 : this.PointD - this.PointC)), 0);
    }
}
