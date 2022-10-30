using Application.LogicInterfaces;
using Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic UserLogic;
    private readonly IConfiguration Config;

    public UserController(IUserLogic userLogic, IConfiguration config)
    {
        UserLogic = userLogic;
        Config = config;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User user = await UserLogic.CreateAsync(dto);
            return Created($"/users/{user.Id}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}