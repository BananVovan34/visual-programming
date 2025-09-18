using System;

namespace visualprogramming.Lab3
{
    public class Rectangle : Figure
    {
        private double _length;
        private double _width;
        
        public double Length => _length;
        public double Width => _width;

        public Rectangle(double length, double width)
        {
            if (length < 0) throw new ArgumentOutOfRangeException(nameof(length), "Length must be greater than or equal to 0");
            if (width < 0) throw new ArgumentOutOfRangeException(nameof(width), "Width must be greater than or equal to 0");
            
            _length = length;
            _width = width;
        }
        
        public override double CalculateArea() => _length * _width;
        
        public override double CalculatePerimeter() => 2 * _length + 2 * _width;
    }
}