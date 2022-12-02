using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.Authorization;
using Shared.Dtos;
using Shared.Models;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    
    private readonly IUserLogic UserLogic;
    private readonly IConfiguration Config;

    public AuthController(IUserLogic userLogic, IConfiguration config)
    {
        UserLogic = userLogic;
        Config = config;
    }
    
    private List<Claim> GenerateClaims(User user) {  
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, Config["Jwt:Subject"]),    
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),      
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),      
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("Email", user.Email),
            new Claim("Role", user.Role.ToString()),
            new Claim("UserId", user.Id.ToString())
        };
        return claims.ToList(); }
    
    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            Config["Jwt:Issuer"],
            Config["Jwt:Audience"],
            claims, 
            null,
            DateTime.UtcNow.AddMinutes(60));
    
        JwtSecurityToken token = new JwtSecurityToken(header, payload);
    
        string serializedToken = new JwtSecurityTokenHandler().WriteToken(token);
        return serializedToken;
    }


    [HttpPost, Route("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto dto)
    {
        try
        {
            User user = await UserLogic.ValidateUser(dto.Username, dto.Password);
            string token = GenerateJwt(user);

            return Ok(token);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}