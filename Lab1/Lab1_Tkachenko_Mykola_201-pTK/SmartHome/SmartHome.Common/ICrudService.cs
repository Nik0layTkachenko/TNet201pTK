using System;
using System.Collections.Generic;

namespace SmartHome.Common
{
    public interface ICrudService<T>
    {
        void Create(T element);
        T Read(Guid id);
        IEnumerable<T> ReadAll();
        void Update(T element);
        void Remove(T element);
        
        // Додаткове завдання
        void Save(string filePath);
        void Load(string filePath);
    }

    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
