using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHome.Common;

namespace SmartHome.NoSql
{
    public class MongoRepository<T> : IRepository<T> where T : class
    {
        public Task<T> GetByIdAsync(Guid id) => Task.FromResult<T>(null);
        public Task<IEnumerable<T>> GetAllAsync() => Task.FromResult<IEnumerable<T>>(new List<T>());
        public Task AddAsync(T entity) { Console.WriteLine("[NoSQL] Додано в MongoDB"); return Task.CompletedTask; }
        public Task Update(T entity) => Task.CompletedTask;
        public Task Delete(T entity) => Task.CompletedTask;
    }
}