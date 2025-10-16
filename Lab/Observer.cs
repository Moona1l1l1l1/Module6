using System;
using System.Collections.Generic;

public interface IObserver
{
    void Update(float temperature);
}

public interface ISubject
{
    void RegisterObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers();
}

public class WeatherStation : ISubject
{
    private List<IObserver> observers = new List<IObserver>();
    private float temperature;

    public void RegisterObserver(IObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IObserver observer)
    {
        if (!observers.Remove(observer))
            Console.WriteLine("Такого наблюдателя нет");
    }

    public void NotifyObservers()
    {
        foreach (var o in observers)
            o.Update(temperature);
    }

    public void SetTemperature(float t)
    {
        if (t < -100 || t > 100)
        {
            Console.WriteLine("Ошибка: температура вне допустимого диапазона");
            return;
        }

        temperature = t;
        Console.WriteLine($"Температура изменилась: {t}°C");
        NotifyObservers();
    }
}

public class WeatherDisplay : IObserver
{
    private string name;

    public WeatherDisplay(string n)
    {
        name = n;
    }

    public void Update(float t)
    {
        Console.WriteLine($"{name} показывает температуру: {t}°C");
    }
}

public class EmailAlert : IObserver
{
    public void Update(float t)
    {
        Console.WriteLine($"Email-оповещение: новая температура {t}°C");
    }
}

class ObserverDemo
{
    static void Main()
    {
        WeatherStation station = new WeatherStation();
        WeatherDisplay app = new WeatherDisplay("Мобильное приложение");
        WeatherDisplay board = new WeatherDisplay("Электронное табло");
        EmailAlert email = new EmailAlert();

        station.RegisterObserver(app);
        station.RegisterObserver(board);
        station.RegisterObserver(email);

        station.SetTemperature(24.5f);
        station.SetTemperature(30.2f);

        station.RemoveObserver(board);
        station.SetTemperature(28.0f);
    }
}
