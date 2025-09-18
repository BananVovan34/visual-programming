using System;

namespace visualprogramming.Lab3
{
    public class Circle : Figure
    {
        private double _radius;
        
        public double Radius => _radius;

        public Circle(double radius)
        {
            if (radius < 0) throw new ArgumentOutOfRangeException(nameof(radius), "Radius must be greater than or equal to 0");
            
            _radius = radius;
        }

        public override double CalculateArea() => Math.PI * _radius * _radius;

        public override double CalculatePerimeter() => 2 * Math.PI * _radius;
    }
}