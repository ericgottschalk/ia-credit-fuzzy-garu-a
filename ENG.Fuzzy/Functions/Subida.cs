using System;
using System.Collections.Generic;
using System.Text;

namespace ENG.Fuzzy.Functions
{
    public class Subida : Constructor
    {
        public Subida(level level, float pointA, float pointB, float pointC)
            : base(level)
        {
            this.Begin = pointA;
            this.End = pointC;
            this.PointA = pointA;
            this.PointB = pointB;
            this.PointC = pointC;
            this.PointD = pointC;
        }
    }
}
