namespace visualprogramming.Lab4;

public class TestLab4
{
    public static void Test()
    {
        GenericsRing<int> ring1 = new GenericsRing<int>(3);
        ring1.Push(1);
        ring1.Push(2);
        ring1.Push(3);

        GenericsRing<int> ring2 = new GenericsRing<int>(3);
        ring2.Push(1);
        ring2.Push(2);
        ring2.Push(3);

        GenericsRing<int> ring3 = new GenericsRing<int>(3);
        ring3.Push(2);
        ring3.Push(3);
        ring3.Push(4);

        Console.WriteLine("ring1 == ring2 ? " + ring1.Equals(ring2));
        Console.WriteLine("ring1 == ring3 ? " + ring1.Equals(ring3));

        Console.WriteLine("Сравнение ring1 и ring2 (по Count): " + ring1.CompareTo(ring2));
        Console.WriteLine("Сравнение ring1 и пустого: " + ring1.CompareTo(new GenericsRing<int>(3)));

        Console.WriteLine("\nПеребор foreach (ring1):");
        
        foreach (var item in ring1)
        {
            Console.WriteLine(item);
        }
    }
}