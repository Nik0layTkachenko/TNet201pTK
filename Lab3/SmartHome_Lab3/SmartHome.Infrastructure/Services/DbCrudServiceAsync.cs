using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartHome.Common;
using SmartHome.Infrastructure.Models;

namespace SmartHome.Infrastructure.Services
{
    public class DbCrudServiceAsync<T> : ICrudServiceAsync<T> where T : DeviceModel
    {
        private readonly IRepository<T> _repository;

        public DbCrudServiceAsync(IRepository<T> repository) { _repository = repository; }

        public async Task<bool> CreateAsync(T element) { try { await _repository.AddAsync(element); return true; } catch { return false; } }
        public async Task<T> ReadAsync(Guid id) => await _repository.GetByIdAsync(id);
        public async Task<IEnumerable<T>> ReadAllAsync() => await _repository.GetAllAsync();
        public async Task<IEnumerable<T>> ReadAllAsync(int page, int amount) { var all = await _repository.GetAllAsync(); return all.Skip((page - 1) * amount).Take(amount); }
        public async Task<bool> UpdateAsync(T element) { try { await _repository.Update(element); return true; } catch { return false; } }
        public async Task<bool> RemoveAsync(T element) { try { await _repository.Delete(element); return true; } catch { return false; } }
        public Task<bool> SaveAsync() => Task.FromResult(true); 
    }
}