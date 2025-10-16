using System;

interface ICost
{
    decimal Calc(double d, string c, int p, bool disc);
}

class Plane : ICost
{
    public decimal Calc(double d, string c, int p, bool disc)
    {
        decimal cost = (decimal)d * 0.25m;
        if (c == "business") cost *= 1.8m;
        if (disc) cost *= 0.9m;
        return cost * p;
    }
}

class Train : ICost
{
    public decimal Calc(double d, string c, int p, bool disc)
    {
        decimal cost = (decimal)d * 0.1m;
        if (c == "business") cost *= 1.5m;
        if (disc) cost *= 0.85m;
        return cost * p;
    }
}

class Bus : ICost
{
    public decimal Calc(double d, string c, int p, bool disc)
    {
        decimal cost = (decimal)d * 0.05m;
        if (c == "business") cost *= 1.3m;
        if (disc) cost *= 0.8m;
        return cost * p;
    }
}

class Context
{
    ICost s;
    public void Set(ICost x) => s = x;
    public decimal Get(double d, string c, int p, bool disc)
    {
        return s.Calc(d, c, p, disc);
    }
}

class Program
{
    static void Main()
    {
        var ctx = new Context();
        Console.WriteLine("plane/train/bus:");
        string t = Console.ReadLine();
        if (t == "plane") ctx.Set(new Plane());
        else if (t == "train") ctx.Set(new Train());
        else ctx.Set(new Bus());

        Console.Write("km: ");
        double d = double.Parse(Console.ReadLine());
        Console.Write("class (econom/business): ");
        string c = Console.ReadLine();
        Console.Write("passengers: ");
        int p = int.Parse(Console.ReadLine());
        Console.Write("discount (y/n): ");
        bool disc = Console.ReadLine() == "y";

        Console.WriteLine("Cost: " + ctx.Get(d, c, p, disc));
    }
}
