using Microsoft.AspNetCore.Mvc;
using SmartHome.REST.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartHome.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        // Static list to mock DB for demonstration of CRUD
        private static List<LightBulbDTO> _devices = new List<LightBulbDTO>
        {
            new LightBulbDTO { Id = Guid.NewGuid(), Name = "Living Room Light", IsOn = false, DeviceType = "LightBulb", Brightness = 80, Color = "White" }
        };

        // GET: api/devices
        [HttpGet]
        public ActionResult<IEnumerable<LightBulbDTO>> GetAll([FromQuery] int page = 1, [FromQuery] int amount = 10)
        {
            var pagedData = _devices.Skip((page - 1) * amount).Take(amount);
            return Ok(pagedData);
        }

        // GET: api/devices/{id}
        [HttpGet("{id}")]
        public ActionResult<LightBulbDTO> GetById(Guid id)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
            if (device == null) return NotFound($"Device with ID {id} not found.");
            return Ok(device);
        }

        // POST: api/devices
        [HttpPost]
        public ActionResult<LightBulbDTO> Create([FromBody] LightBulbDTO newDevice)
        {
            newDevice.Id = Guid.NewGuid();
            _devices.Add(newDevice);
            return CreatedAtAction(nameof(GetById), new { id = newDevice.Id }, newDevice);
        }

        // PUT: api/devices/{id}
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, [FromBody] LightBulbDTO updatedDevice)
        {
            var index = _devices.FindIndex(d => d.Id == id);
            if (index < 0) return NotFound();

            updatedDevice.Id = id; // Ensure ID matches
            _devices[index] = updatedDevice;
            return NoContent();
        }

        // DELETE: api/devices/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var device = _devices.FirstOrDefault(d => d.Id == id);
            if (device == null) return NotFound();

            _devices.Remove(device);
            return NoContent();
        }
    }
}