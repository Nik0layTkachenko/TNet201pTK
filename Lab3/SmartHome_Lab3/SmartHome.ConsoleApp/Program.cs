using System;
using System.Linq;
using System.Threading.Tasks;
using SmartHome.Common;
using SmartHome.Infrastructure.Data;
using SmartHome.Infrastructure.Models;
using SmartHome.Infrastructure.Repositories;
using SmartHome.Infrastructure.Services;
using SmartHome.NoSql;

namespace SmartHome.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== SmartHome System - Лабораторна робота №3 ===");
            Console.WriteLine("Виконав: студент групи 201-пТК Ткаченко Микола Віталійович (м. Полтава)\n");

            using var context = new SmartHomeContext();
            
            Console.WriteLine("Ініціалізація бази даних SQLite (Entity Framework Core)...");
            // Ці два рядки створюють файл smarthome.db автоматично!
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync(); 
            Console.WriteLine("Базу даних 'smarthome.db' успішно створено та застосовано схему!\n");

            var bulbRepo = new Repository<LightBulbModel>(context);
            var bulbService = new DbCrudServiceAsync<LightBulbModel>(bulbRepo);

            Console.WriteLine("Створення об'єктів та збереження в БД...");
            var room = new RoomModel { Id = Guid.NewGuid(), Name = "Вітальня" };
            context.Rooms.Add(room);
            await context.SaveChangesAsync();

            var bulb = new LightBulbModel 
            { 
                Id = Guid.NewGuid(), 
                Name = "Стельова лампа", 
                Brightness = 80, 
                Color = "White",
                RoomId = room.Id,
                Settings = new BulbSettingsModel { Id = Guid.NewGuid(), PowerSavingMode = "Eco" }
            };
            
            await bulbService.CreateAsync(bulb);
            Console.WriteLine($"-> Лампочка '{bulb.Name}' успішно збережена в БД.");

            var allBulbs = await bulbService.ReadAllAsync();
            Console.WriteLine($"\nЧитання з БД: Знайдено {allBulbs.Count()} пристроїв.");
            foreach(var b in allBulbs) {
                Console.WriteLine($"- {b.Name} (Яскравість: {b.Brightness}%)");
            }

            Console.WriteLine("\n--- Додаткове завдання (NoSQL) ---");
            var mongoRepo = new MongoRepository<RoomModel>();
            await mongoRepo.AddAsync(room);
        }
    }
}