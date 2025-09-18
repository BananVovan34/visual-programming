using System;

namespace visualprogramming.Lab3
{
    public class Triangle : Figure
    {
        private double _a;
        private double _b;
        private double _c;

        public double SideA => _a;
        public double SideB => _b;
        public double SideC => _c;
        
        public double HalfPerimeter => CalculatePerimeter() / 2;

        public Triangle(double a, double b, double c)
        {
            if (a < 0) throw new ArgumentOutOfRangeException(nameof(a), "Side A must be greater than or equal to 0");
            if (b < 0) throw new ArgumentOutOfRangeException(nameof(b), "Side B must be greater than or equal to 0");
            if (c < 0) throw new ArgumentOutOfRangeException(nameof(c), "Side B must be greater than or equal to 0");
            
            if (a + b < c || a + c < b || b + c < a) throw new ArgumentOutOfRangeException("The sum of the lengths of the two sides must be greater than the third");
            
            _a = a;
            _b = b;
            _c = c;
        }

        public override double CalculateArea() =>
            Math.Sqrt(HalfPerimeter * (HalfPerimeter -  _a) * (HalfPerimeter - _b) * (HalfPerimeter - _c));

        public override double CalculatePerimeter() => _a + _b + _c;
        
    }
}