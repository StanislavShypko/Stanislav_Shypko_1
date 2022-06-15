using System;
using System.IO;
using System.Text.Json;

namespace Laba1;

public static class Program
{
    public static void Main()
    {
        var fr1 = new Fraction(1, 2);
        var fr2 = new Fraction(2, 3);
        Console.WriteLine(fr1);
        Console.WriteLine(fr2);

        Console.WriteLine(fr1 + fr2);
        Console.WriteLine(fr1 * 2);
        Console.WriteLine(fr1 / 2);

        /* ------- 2-ге завдання ------ */
        var createStream = File.Create("droby.json");
        JsonSerializer.Serialize(createStream, fr1);
        createStream.Dispose();
        using var openStream = File.OpenRead("droby.json");
        var deserialized = JsonSerializer.Deserialize<Fraction>(openStream);

        Console.WriteLine(JsonSerializer.Serialize(fr1));
        Console.WriteLine(deserialized);
    }
}

public class Fraction
{
    public double Numerator { get; set; }
    public double Denumerator { get; set; }

    public Fraction(double numerator, double denumerator)
    {
        Numerator = numerator;
        Denumerator = denumerator;
    }

    public static Fraction operator +(Fraction first, Fraction second)
    {
        if (first.Denumerator == second.Denumerator)
        {
            return new Fraction(first.Numerator + second.Numerator, first.Denumerator);
        }

        var nsk = first.Denumerator * second.Denumerator;
        return new Fraction(first.Numerator * second.Denumerator + 
                            second.Numerator * first.Denumerator, 
            nsk);
    }
    
    public static Fraction operator *(Fraction fraction, double num)
    {
            return new Fraction(fraction.Numerator * num, fraction.Denumerator);
    }
    
    public static Fraction operator /(Fraction fraction, double num)
    {
        return new Fraction(fraction.Numerator / num, fraction.Denumerator);
    }
    
    public static Fraction operator *(Fraction first, Fraction second)
    {
        return new Fraction(first.Numerator * second.Numerator, first.Denumerator * first.Denumerator);
    }

    public override string ToString()
    {
        return $"{Numerator}/{Denumerator}";
    }
}