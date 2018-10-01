using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.Fuzzy.Functions
{
    public class Trapezoidal : Constructor
    {
        public Trapezoidal(level level, float pointA, float pointB, float pointC, float pointD)
            : base(level)
        {
            this.Begin = pointA;
            this.End = pointD;
            this.PointA = pointA;
            this.PointB = pointB;
            this.PointC = pointC;
            this.PointD = pointD;
        }
    }
}
