using Microsoft.AspNetCore.Mvc;

namespace MyAppApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HomeController : ControllerBase
{
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok("Hello Users");
    }
}