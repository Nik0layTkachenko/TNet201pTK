using System;
namespace SmartHome.REST.Models
{
    public class DeviceDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsOn { get; set; }
        public string DeviceType { get; set; } // "LightBulb" or "Thermostat"
    }

    public class LightBulbDTO : DeviceDTO
    {
        public int Brightness { get; set; }
        public string Color { get; set; }
    }
}