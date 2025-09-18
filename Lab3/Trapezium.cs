using System;

namespace visualprogramming.Lab3
{
    public class Trapezium : Figure
    {
        private double _a;
        private double _b;
        private double _c;
        private double _d;
        private double _height;

        public double SideA => _a;
        public double SideB => _b;
        public double SideC => _c;
        public double SideD => _d;
        public double Height => _height;
        
        public Trapezium(double a, double b, double c, double d, double height)
        {
            if (a < 0) throw new ArgumentOutOfRangeException(nameof(a), "Side A must be greater than or equal to 0");
            if (b < 0) throw new ArgumentOutOfRangeException(nameof(b), "Side B must be greater than or equal to 0");
            if (c < 0) throw new ArgumentOutOfRangeException(nameof(c), "Side C must be greater than or equal to 0");
            if (d < 0) throw new ArgumentOutOfRangeException(nameof(d), "Side D must be greater than or equal to 0");
            if (height < 0) throw new ArgumentOutOfRangeException(nameof(height), "Side B must be greater than or equal to 0");
            
            _a = a;
            _b = b;
            _c = c;
            _d = d;
            _height = height;
        }

        public override double CalculateArea() => 0.5 * (_a + _b) * _height;

        public override double CalculatePerimeter() => _a + _b + _c + _d;
    }
}