using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CircuitFunctionMaker
{
    class FunctionPont : IEquatable<FunctionPont>, IComparable<FunctionPont>
    {
        public double corner;
        public Radius radius;
        public FunctionPont(double corner, Radius radius)
        {
            this.corner = corner;
            this.radius = radius;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            FunctionPont objAsFunctionPont = obj as FunctionPont;
            if (objAsFunctionPont == null) return false;
            else return Equals(objAsFunctionPont);
        }

        public int CompareTo(FunctionPont compareFunctionPont)
        {
            if (compareFunctionPont == null)
                return 1;

            else
                return this.corner.CompareTo(compareFunctionPont.corner);
        }

        public bool Equals(FunctionPont other)
        {
            if (other == null) return false;
            return (this.corner.Equals(other.corner));
        }
    }
}
