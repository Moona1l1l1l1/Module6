using System;

public interface IShippingStrategy
{
    decimal CalculateShippingCost(decimal weight, decimal distance);
}

public class StandardShippingStrategy : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return weight * 0.5m + distance * 0.1m;
    }
}

public class ExpressShippingStrategy : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return (weight * 0.75m + distance * 0.2m) + 10;
    }
}

public class InternationalShippingStrategy : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return weight * 1.0m + distance * 0.5m + 15;
    }
}

public class NightShippingStrategy : IShippingStrategy
{
    public decimal CalculateShippingCost(decimal weight, decimal distance)
    {
        return (weight * 0.8m + distance * 0.3m) + 20;
    }
}

public class DeliveryContext
{
    private IShippingStrategy strategy;

    public void SetShippingStrategy(IShippingStrategy s)
    {
        strategy = s;
    }

    public decimal CalculateCost(decimal weight, decimal distance)
    {
        if (strategy == null)
        {
            throw new InvalidOperationException("Стратегия не выбрана");
        }
        return strategy.CalculateShippingCost(weight, distance);
    }
}

class StrategyDemo
{
    static void Main()
    {
        DeliveryContext delivery = new DeliveryContext();

        Console.WriteLine("Выберите тип доставки: 1 - Стандарт, 2 - Экспресс, 3 - Международная, 4 - Ночная");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                delivery.SetShippingStrategy(new StandardShippingStrategy());
                break;
            case "2":
                delivery.SetShippingStrategy(new ExpressShippingStrategy());
                break;
            case "3":
                delivery.SetShippingStrategy(new InternationalShippingStrategy());
                break;
            case "4":
                delivery.SetShippingStrategy(new NightShippingStrategy());
                break;
            default:
                Console.WriteLine("Неверный выбор");
                return;
        }

        Console.Write("Вес (кг): ");
        decimal w = Convert.ToDecimal(Console.ReadLine());
        Console.Write("Расстояние (км): ");
        decimal d = Convert.ToDecimal(Console.ReadLine());

        if (w < 0 || d < 0)
        {
            Console.WriteLine("Ошибка: отрицательные значения недопустимы");
            return;
        }

        decimal cost = delivery.CalculateCost(w, d);
        Console.WriteLine($"Стоимость доставки: {cost}₸");
    }
}
