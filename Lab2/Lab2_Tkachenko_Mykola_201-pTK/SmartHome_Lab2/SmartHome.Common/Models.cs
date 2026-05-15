using System;
namespace SmartHome.Common {
 public abstract class Device : IEntity { public Guid Id { get; set; } public string Name { get; set; } public bool IsOn { get; set; } protected Device(string name) { Id = Guid.NewGuid(); Name = name; IsOn = false; } }
 public class LightBulb : Device { public string Color { get; set; } public int Brightness { get; set; } public int Wattage { get; set; } private static Random _rnd = new Random(); public LightBulb(string name, string color, int wattage) : base(name) { Color = color; Wattage = wattage; Brightness = 100; } public LightBulb() : base("Default Bulb") { }
 public static LightBulb CreateNew() { string[] colors = { "White", "Warm White", "Red", "Blue", "Green" }; var b = new LightBulb($"Bulb_{Guid.NewGuid().ToString().Substring(0, 5)}", colors[_rnd.Next(colors.Length)], _rnd.Next(5, 100)); b.Brightness = _rnd.Next(10, 100); return b; } }
}