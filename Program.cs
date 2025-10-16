using System;
using System.Collections.Generic;

public interface IPaymentStrategy
{
    void Pay(decimal amount);
}

public class CreditCardPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата {amount} тенге банковской картой выполнена");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата {amount} тенге через PayPal выполнена");
    }
}

public class CryptoPayment : IPaymentStrategy
{
    public void Pay(decimal amount)
    {
        Console.WriteLine($"Оплата {amount} тенге криптовалютой выполнена");
    }
}

public class PaymentContext
{
    private IPaymentStrategy strategy;

    public void SetPaymentStrategy(IPaymentStrategy s)
    {
        strategy = s;
    }

    public void Pay(decimal amount)
    {
        if (strategy == null)
        {
            Console.WriteLine("Стратегия не выбрана");
            return;
        }
        strategy.Pay(amount);
    }
}

public interface IObserver
{
    void Update(string currency, decimal rate);
}

public interface ISubject
{
    void Attach(IObserver o);
    void Detach(IObserver o);
    void Notify();
}

public class CurrencyExchange : ISubject
{
    private List<IObserver> observers = new List<IObserver>();
    private string currency;
    private decimal rate;

    public void SetRate(string c, decimal r)
    {
        currency = c;
        rate = r;
        Console.WriteLine($"\nНовый курс: {currency} = {rate}");
        Notify();
    }

    public void Attach(IObserver o)
    {
        observers.Add(o);
    }

    public void Detach(IObserver o)
    {
        observers.Remove(o);
    }

    public void Notify()
    {
        foreach (var o in observers)
            o.Update(currency, rate);
    }
}

public class EmailSubscriber : IObserver
{
    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"Email: {currency} = {rate}");
    }
}

public class WebDisplay : IObserver
{
    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"Сайт: {currency} = {rate}");
    }
}

public class MobileApp : IObserver
{
    public void Update(string currency, decimal rate)
    {
        Console.WriteLine($"Мобильное приложение: {currency} = {rate}");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Выберите паттерн:");
        Console.WriteLine("1 - Стратегия (оплата)");
        Console.WriteLine("2 - Наблюдатель (курсы валют)");
        Console.Write("Выбор: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            RunStrategy();
        }
        else if (choice == "2")
        {
            RunObserver();
        }
        else
        {
            Console.WriteLine("Неверный выбор");
        }
    }

    static void RunStrategy()
    {
        PaymentContext context = new PaymentContext();

        Console.WriteLine("1 - карта");
        Console.WriteLine("2 - PayPal");
        Console.WriteLine("3 - криптовалюта");
        Console.Write("Выбор: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                context.SetPaymentStrategy(new CreditCardPayment());
                break;
            case "2":
                context.SetPaymentStrategy(new PayPalPayment());
                break;
            case "3":
                context.SetPaymentStrategy(new CryptoPayment());
                break;
            default:
                Console.WriteLine("Неверный выбор");
                return;
        }

        Console.Write("Введите сумму: ");
        decimal amount = Convert.ToDecimal(Console.ReadLine());

        context.Pay(amount);
    }

    static void RunObserver()
    {
        CurrencyExchange exchange = new CurrencyExchange();

        var email = new EmailSubscriber();
        var web = new WebDisplay();
        var mobile = new MobileApp();

        exchange.Attach(email);
        exchange.Attach(web);
        exchange.Attach(mobile);

        exchange.SetRate("USD", 480.5m);
        exchange.SetRate("EUR", 510.2m);

        exchange.Detach(web);
        Console.WriteLine("\nWebDisplay отписан");

        exchange.SetRate("USD", 485.7m);
    }
}
