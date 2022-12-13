using Application.LogicInterfaces;
using GrpcService1;
using Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shared.Models;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserLogic _userLogic;

    public UserController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    [HttpPost]
    public async Task<ActionResult<User>> CreateAsync(UserCreationDto dto)
    {
        try
        {
            User user = await _userLogic.CreateAsync(dto);
            return Created($"/users/{user.Id}", user);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<User>> GetAsync([FromQuery] string? username)
    {
        try
        {
            User user = await _userLogic.GetUser(username);
            return user;
        }
        
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<User>> DeleteUser(IntRequest userId)
    {
        try
        {
            User user = await _userLogic.DeleteUser(userId.Int);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}