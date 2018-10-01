using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.Fuzzy.Functions
{
    public class Descida : Constructor
    {
        public Descida(level level, float pointA, float pointB, float pointC)
            : base(level)
        {
            this.Begin = pointA;
            this.End = pointC;
            this.PointA = pointA;
            this.PointB = pointA;
            this.PointC = pointB;
            this.PointD = pointC;
        }
    }
}
