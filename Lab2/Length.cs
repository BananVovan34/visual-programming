namespace visualprogramming.lab2
{
    public class Length
    {
        private long _millimeters;

        public double LengthInKilometers => _millimeters / 1_000_000.0;
        public double LengthInMeters => _millimeters / 1000.0;
        public double LengthInCentimeters => _millimeters / 10.0;
        public double LengthInMillimeters => _millimeters;
        
        public double LengthInMiles => _millimeters / 1609344.0;
        public double LengthInFoot => _millimeters / 304.8;
        public double LengthInInch => _millimeters / 25.4;

        public Length(uint kilometers, uint meters, uint centimeters, uint millimeters)
        {
            _millimeters = (long)kilometers * 1_000_000 
                           + (long)meters * 1000
                           + (long)centimeters * 10
                           + millimeters;
        }

        public Length(double meters) =>
            this._millimeters = (long)Math.Round(meters * 1000.0);

        public override string ToString() => _millimeters.ToString();
        
        public static Length operator +(Length a, Length b) =>
            new Length((a.LengthInMillimeters + b.LengthInMillimeters) / 1000.0);
        public static Length operator +(Length a, int b) =>
            new Length(a.LengthInMillimeters + b);
        public static Length operator +(Length a, float b) =>
            new Length(a.LengthInMillimeters + b);
        public static Length operator +(Length a, double b) =>
            new Length(a.LengthInMillimeters + b);
        
        public static Length operator -(Length a, Length b) =>
            new Length((a.LengthInMillimeters - b.LengthInMillimeters) / 1000.0);
        public static Length operator -(Length a, int b) =>
            new Length(a.LengthInMillimeters - b);
        public static Length operator -(Length a, float b) =>
            new Length(a.LengthInMillimeters - b);
        public static Length operator -(Length a, double b) =>
            new Length(a.LengthInMillimeters - b);
        
        public static Length operator *(Length a, int b) =>
            new Length(a.LengthInMillimeters * b);
        public static Length operator *(Length a, float b) =>
            new Length(a.LengthInMillimeters * b);
        public static Length operator *(Length a, double b) =>
            new Length(a.LengthInMillimeters * b);
        
        public static Length operator /(Length a, int b) =>
            new Length(a.LengthInMillimeters / b);
        public static Length operator /(Length a, float b) =>
            new Length(a.LengthInMillimeters / b);
        public static Length operator /(Length a, double b) =>
            new Length(a.LengthInMillimeters / b);
        
    }
}