using System;
using System.Collections.Generic;

interface IObs { void Upd(string s, decimal p); }
interface ISub
{
    void Add(string s, IObs o);
    void Rem(string s, IObs o);
    void Set(string s, decimal p);
}

class Stock : ISub
{
    Dictionary<string, List<IObs>> obs = new();
    Dictionary<string, decimal> price = new();

    public void Add(string s, IObs o)
    {
        if (!obs.ContainsKey(s)) obs[s] = new List<IObs>();
        obs[s].Add(o);
    }

    public void Rem(string s, IObs o)
    {
        if (obs.ContainsKey(s)) obs[s].Remove(o);
    }

    public void Set(string s, decimal p)
    {
        price[s] = p;
        if (obs.ContainsKey(s))
            foreach (var o in obs[s]) o.Upd(s, p);
    }
}

class T1 : IObs
{
    public void Upd(string s, decimal p)
    {
        Console.WriteLine($"{s} = {p}");
    }
}

class Bot : IObs
{
    public void Upd(string s, decimal p)
    {
        if (p < 100) Console.WriteLine($"{s}: buy");
        else if (p > 200) Console.WriteLine($"{s}: sell");
    }
}

class Program2
{
    static void Main()
    {
        var ex = new Stock();
        var o1 = new T1();
        var o2 = new Bot();

        ex.Add("AAPL", o1);
        ex.Add("AAPL", o2);
        ex.Add("GOOG", o1);

        ex.Set("AAPL", 120);
        ex.Set("GOOG", 300);
        ex.Set("AAPL", 90);

        ex.Rem("AAPL", o1);
        ex.Set("AAPL", 250);
    }
}
