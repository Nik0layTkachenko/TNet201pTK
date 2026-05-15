using System;
using System.Linq;
using System.Threading.Tasks;
using SmartHome.Common;
using Xunit;
namespace SmartHome.Tests {
 public class CrudServiceTests {
 [Fact]
 public async Task CreateAsync_ShouldAddElement() {
 var service = new GenericCrudServiceAsync<LightBulb>("test.json");
 var bulb = LightBulb.CreateNew();
 var result = await service.CreateAsync(bulb);
 Assert.True(result);
 }
 }
}