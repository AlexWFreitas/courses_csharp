using System;

namespace Polygons.Library
{
    public class ConcreteRegularPolygon
    {
        private int _sideLength;                // Backing field for the Full Property
        public int NumberOfSides { get; set; }  // Automatic Property ( Automatically generates the backing field )
        public int SideLength                   // Full property ( Custom getter and setter )
        {
            get { return _sideLength; }
            set { _sideLength = value; }
        }

        public ConcreteRegularPolygon(int sides, int length)
        {
            NumberOfSides = sides;
            SideLength = length;
        }

        public double GetPerimeter()
        {
            return NumberOfSides * SideLength;
        }

        public virtual double GetArea()
        {
            throw new NotImplementedException();
        }
    }
}
