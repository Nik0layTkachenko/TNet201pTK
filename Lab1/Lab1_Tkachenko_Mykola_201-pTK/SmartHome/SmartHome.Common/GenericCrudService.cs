using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SmartHome.Common
{
    public class GenericCrudService<T> : ICrudService<T> where T : IEntity
    {
        private List<T> _items;

        public GenericCrudService()
        {
            _items = new List<T>();
        }

        public void Create(T element)
        {
            _items.Add(element);
        }

        public T Read(Guid id)
        {
            return _items.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> ReadAll()
        {
            return _items;
        }

        public void Update(T element)
        {
            var index = _items.FindIndex(e => e.Id == element.Id);
            if (index != -1)
            {
                _items[index] = element;
            }
        }

        public void Remove(T element)
        {
            _items.Remove(element);
        }

        public void Save(string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(_items, options);
            File.WriteAllText(filePath, json);
        }

        public void Load(string filePath)
        {
            if (File.Exists(filePath))
                {
                var json = File.ReadAllText(filePath);
                _items = JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
            }
        }
    }
}
