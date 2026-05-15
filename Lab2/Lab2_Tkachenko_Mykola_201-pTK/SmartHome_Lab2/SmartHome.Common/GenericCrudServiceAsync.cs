using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
namespace SmartHome.Common {
 public class GenericCrudServiceAsync<T> : ICrudServiceAsync<T> where T : IEntity {
 private readonly ConcurrentDictionary<Guid, T> _items;
 private readonly string _filePath;
 private readonly SemaphoreSlim _fileLock = new SemaphoreSlim(1, 1);
 public GenericCrudServiceAsync(string filePath) { _items = new ConcurrentDictionary<Guid, T>(); _filePath = filePath; }
 public Task<bool> CreateAsync(T element) { return Task.FromResult(_items.TryAdd(element.Id, element)); }
 public Task<T> ReadAsync(Guid id) { _items.TryGetValue(id, out T val); return Task.FromResult(val); }
 public Task<IEnumerable<T>> ReadAllAsync() { return Task.FromResult(_items.Values.AsEnumerable()); }
 public Task<IEnumerable<T>> ReadAllAsync(int page, int amount) { return Task.FromResult(_items.Values.Skip((page - 1) * amount).Take(amount)); }
 public Task<bool> UpdateAsync(T element) { if (_items.ContainsKey(element.Id)) { _items[element.Id] = element; return Task.FromResult(true); } return Task.FromResult(false); }
 public Task<bool> RemoveAsync(T element) { return Task.FromResult(_items.TryRemove(element.Id, out _)); }
 public async Task<bool> SaveAsync() { await _fileLock.WaitAsync(); try { var options = new JsonSerializerOptions { WriteIndented = true }; var json = JsonSerializer.Serialize(_items.Values, options); await File.WriteAllTextAsync(_filePath, json); return true; } catch { return false; } finally { _fileLock.Release(); } }
 public IEnumerator<T> GetEnumerator() { return _items.Values.GetEnumerator(); }
 IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
 }
}