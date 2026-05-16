using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SmartHome.REST.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SecureDevicesController : ControllerBase
    {
        [HttpGet("public-info")]
        public IActionResult GetPublicInfo()
        {
            return Ok("This is accessible to everyone.");
        }

        [Authorize]
        [HttpGet("user-info")]
        public IActionResult GetUserInfo()
        {
            return Ok("Accessible to any authenticated user.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-settings")]
        public IActionResult GetAdminSettings()
        {
            return Ok("Accessible ONLY to users with 'Admin' role.");
        }
    }
}