namespace visualprogramming.Lab3;

public class TestLab3
{
    public static void Test()
    {
        Circle circle = new Circle(5);
        Console.WriteLine("Круг (радиус 5):");
        Console.WriteLine($"Площадь = {circle.CalculateArea()}");
        Console.WriteLine($"Периметр = {circle.CalculatePerimeter()}\n");
        
        Rectangle rect = new Rectangle(10, 4);
        Console.WriteLine("Прямоугольник (10 x 4):");
        Console.WriteLine($"Площадь = {rect.CalculateArea()}");
        Console.WriteLine($"Периметр = {rect.CalculatePerimeter()}\n");

        Trapezium trapezium = new Trapezium(6, 10, 5, 5, 4);
        Console.WriteLine("Трапеция (a=6, b=10, c=5, d=5, h=4):");
        Console.WriteLine($"Площадь = {trapezium.CalculateArea()}");
        Console.WriteLine($"Периметр = {trapezium.CalculatePerimeter()}\n");

        Triangle triangle = new Triangle(3, 4, 5);
        Console.WriteLine("Треугольник (3, 4, 5):");
        Console.WriteLine($"Площадь = {triangle.CalculateArea()}");
        Console.WriteLine($"Периметр = {triangle.CalculatePerimeter()}\n");

        try
        {
            Console.WriteLine("Создание круга с отрицательным радиусом:");
            Circle badCircle = new Circle(-3);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}\n");
        }

        try
        {
            Console.WriteLine("Создание невозможного треугольника (1, 2, 10):");
            Triangle badTriangle = new Triangle(1, 2, 10);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}\n");
        }
    }
}