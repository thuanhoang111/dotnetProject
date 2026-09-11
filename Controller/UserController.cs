using Microsoft.AspNetCore.Mvc;
using MyAppApi.Models;

namespace MyAppApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static readonly List<User> Users = new()
    {
        new User
        {
            Id = 1,
            Name = "John",
            Email = "john@gmail.com",
            Age = 25
        },
        new User
        {
            Id = 2,
            Name = "Peter",
            Email = "peter@gmail.com",
            Age = 30
        }
    };

    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(Users);
    }
}