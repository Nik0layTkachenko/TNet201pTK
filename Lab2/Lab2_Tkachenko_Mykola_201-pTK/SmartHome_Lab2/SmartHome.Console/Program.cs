using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SmartHome.Common;
namespace SmartHome.ConsoleApp {
 class Program {
 static async Task Main(string[] args) {
 Console.OutputEncoding = System.Text.Encoding.UTF8;
 Console.WriteLine("=== SmartHome System - Лабораторна робота №2 ===");
 Console.WriteLine("Виконав: студент групи 201-пТК Ткаченко Микола Віталійович (м. Полтава)\n");
 var filePath = "async_bulbs_data.json";
 ICrudServiceAsync<LightBulb> bulbService = new GenericCrudServiceAsync<LightBulb>(filePath);
 Console.WriteLine("Починаємо паралельне створення 1000 об'єктів...");
 Parallel.For(0, 1000, i => { bulbService.CreateAsync(LightBulb.CreateNew()).Wait(); });
 Console.WriteLine($"Успішно створено {bulbService.Count()} об'єктів.\n");
 var minWattage = bulbService.Min(b => b.Wattage);
 var maxWattage = bulbService.Max(b => b.Wattage);
 var avgWattage = bulbService.Average(b => b.Wattage);
 Console.WriteLine("--- Статистика (LINQ) ---");
 Console.WriteLine($"Wattage: Min={minWattage}W, Max={maxWattage}W, Avg={avgWattage:F2}W");
 Console.WriteLine("\n--- Пагінація ---");
 var pagedItems = await bulbService.ReadAllAsync(2, 3);
 foreach (var item in pagedItems) { Console.WriteLine($"- {item.Name} | {item.Wattage}W"); }
 Console.WriteLine("\nЗберігаємо...");
 await bulbService.SaveAsync();
 Console.WriteLine("Збережено.");
 DemoSyncPrimitives();
 }
 static void DemoSyncPrimitives() {
 Console.WriteLine("\n--- Синхронізація ---");
 object _lockObj = new object(); int counter = 0;
 Parallel.For(0, 100, i => { lock (_lockObj) { counter++; } });
 Console.WriteLine($"1. lock: {counter}");
 SemaphoreSlim sem = new SemaphoreSlim(2, 2);
 Parallel.For(0, 4, i => { sem.Wait(); Thread.Sleep(10); sem.Release(); });
 Console.WriteLine("2. SemaphoreSlim: OK");
 AutoResetEvent are = new AutoResetEvent(false);
 Task.Run(() => { Thread.Sleep(50); are.Set(); });
 are.WaitOne();
 Console.WriteLine("3. AutoResetEvent: OK");
 }
 }
}