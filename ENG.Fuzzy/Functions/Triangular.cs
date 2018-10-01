using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.Fuzzy.Functions
{
    public class Triangular : Constructor
    {
        public Triangular(level level, float pointA, float pointB, float pointC)
            : base(level)
        {
            this.Begin = pointA;
            this.End = pointC;
            this.PointA = pointA;
            this.PointB = pointB;
            this.PointC = pointC;
        }

        public override float Value(float x)
            => Math.Max(Math.Min((x - this.PointA) / (this.PointB - this.PointA), (this.PointC - x) / (this.PointC - this.PointB)), 0);
    }
}
