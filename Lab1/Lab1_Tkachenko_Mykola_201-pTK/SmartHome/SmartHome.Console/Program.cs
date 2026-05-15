using System;
using SmartHome.Common;

namespace SmartHome.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== SmartHome System - Лабораторна робота №1 ===");
            Console.WriteLine("Виконав: студент групи 201-пТК Ткаченко Микола Віталійович\n");

            ICrudService<LightBulb> bulbService = new GenericCrudService<LightBulb>();

            var bulb1 = new LightBulb("Світло у вітальні", "White", 60);
            var bulb2 = new LightBulb("Світло у спальні", "Warm White", 40);

            bulb1.OnStateChanged += msg => Console.WriteLine($"[ПОДІЯ] {msg}");
            bulb2.OnStateChanged += msg => Console.WriteLine($"[ПОДІЯ] {msg}");

            bulbService.Create(bulb1);
            bulbService.Create(bulb2);
            Console.WriteLine("Додано дві розумні лампочки до сервісу.");

            bulb1.Toggle(); 
            bulb1.Brightness = 80;

            bulbService.Update(bulb1);

            Console.WriteLine("\nВсі пристрої у системі:");
            foreach (var b in bulbService.ReadAll())
            {
                Console.WriteLine($"- {b.Name}: {(b.IsOn ? "Увімкнено" : "Вимкнено")}, Яскравість: {b.Brightness}%, Колір: {b.Color}");
            }

            Console.WriteLine($"\nЗагальна кількість створених лампочок: {LightBulb.GetTotalBulbs()}");

            string filePath = "smart_bulbs_data.json";
            bulbService.Save(filePath);
            Console.WriteLine($"\n[Файлова система] Дані збережено у файл: {filePath}");

            bulbService.Remove(bulb1);
            Console.WriteLine("Лампочку 1 видалено з пам'яті.");
            
            bulbService.Load(filePath);
            Console.WriteLine("\n[Файлова система] Дані завантажено. Поточний стан:");
            foreach (var b in bulbService.ReadAll())
            {
                Console.WriteLine($"- {b.Name} (ID: {b.Id})");
            }
        }
    }
}
