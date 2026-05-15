using System;

namespace SmartHome.Common
{
    // Делегати та Події
    public delegate void DeviceStateChangedHandler(string message);

    public abstract class Device : IEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }

        // Подія
        public event DeviceStateChangedHandler OnStateChanged;

        // Конструктор
        protected Device(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsOn = false;
        }

        // Метод
        public virtual void TurnOn()
        {
            IsOn = true;
            OnStateChanged?.Invoke($"Пристрій '{Name}' увімкнено.");
        }

        public virtual void TurnOff()
        {
            IsOn = false;
            OnStateChanged?.Invoke($"Пристрій '{Name}' вимкнено.");
        }
    }

    public class LightBulb : Device
    {
        public string Color { get; set; }
        public int Brightness { get; set; }
        public int Wattage { get; set; }

        // Статичні поля
        public static int TotalBulbsCreated;

        // Статичні конструктори
        static LightBulb()
        {
            TotalBulbsCreated = 0;
        }

        // Конструктор
        public LightBulb(string name, string color, int wattage) : base(name)
        {
            Color = color;
            Wattage = wattage;
            Brightness = 100;
            TotalBulbsCreated++;
        }

        // Статичний метод
        public static int GetTotalBulbs()
        {
            return TotalBulbsCreated;
        }
    }

    public class Thermostat : Device
    {
        public double TargetTemperature { get; set; }
        public double CurrentTemperature { get; set; }
        public double Humidity { get; set; }

        public Thermostat(string name) : base(name)
        {
            TargetTemperature = 22.0;
            CurrentTemperature = 20.0;
            Humidity = 45.0;
        }
    }

    public class Room : IEntity
    {
        public Guid Id { get; set; }
        public string RoomName { get; set; }
        public double Area { get; set; }
        public int FloorNumber { get; set; }

        public Room()
        {
            Id = Guid.NewGuid();
        }
    }

    // Метод розширення
    public static class DeviceExtensions
    {
        public static void Toggle(this Device device)
        {
            if (device.IsOn)
                device.TurnOff();
            else
                device.TurnOn();
        }
    }
}
