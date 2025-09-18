namespace visualprogramming.lab2
{
    public class TestLab2
    {
        public static void Test()
        {
            Console.WriteLine("=== Тестирование класса Length ===\n");

            var len1 = new Length(1, 200, 30, 5);
            var len2 = new Length(123.456);

            Console.WriteLine($"len1 (в мм): {len1.LengthInMillimeters}");
            Console.WriteLine($"len1 (в м): {len1.LengthInMeters}");
            Console.WriteLine($"len1 (в км): {len1.LengthInKilometers}");
            Console.WriteLine($"len1 (в дюймах): {len1.LengthInInch}");
            Console.WriteLine($"len1 (в футах): {len1.LengthInFoot}");
            Console.WriteLine($"len1 (в милях): {len1.LengthInMiles}\n");

            Console.WriteLine($"len2 (123.456 м в мм): {len2.LengthInMillimeters}");
            Console.WriteLine($"len2 (в дюймах): {len2.LengthInInch}\n");

            var sum = len1 + len2;
            var diff = len1 - len2;
            var mul = len2 * 3;
            var div = len1 / 2;

            Console.WriteLine($"len1 + len2 = {sum.LengthInMeters} м");
            Console.WriteLine($"len1 - len2 = {diff.LengthInMeters} м");
            Console.WriteLine($"len2 * 3 = {mul.LengthInMeters} м");
            Console.WriteLine($"len1 / 2 = {div.LengthInMeters} м\n");

            var addDouble = len1 + 50.0;
            var subInt = len2 - 10;
            var mulFloat = len2 * 1.5f;
            var divDouble = len2 / 2.5;

            Console.WriteLine($"len1 + 50.0 = {addDouble.LengthInMeters} м");
            Console.WriteLine($"len2 - 10 мм = {subInt.LengthInMillimeters} мм");
            Console.WriteLine($"len2 * 1.5f = {mulFloat.LengthInMeters} м");
            Console.WriteLine($"len2 / 2.5 = {divDouble.LengthInMeters} м\n");
        }
    }
}