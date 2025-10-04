using System;
using System.Collections.Generic;
using System.Linq;
using visualprogramming.lab2;

namespace visualprogramming.Lab7
{
    public static class LengthDatabase
    {
        public static List<Length> lengths = new List<Length>
            {
                new Length(1500.0),    // 1.5 км
                new Length(200),       // 200 м
                new Length(12345.6),   // 12.345 км
                new Length(500),       // 500 м
                new Length(7500),      // 7.5 км
                new Length(50),        // 50 м
                new Length(999999)     // почти 1000 км
            };
    }
}
