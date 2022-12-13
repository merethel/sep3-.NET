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
    
    private readonly IUserLogic _userLogic;
    private readonly IConfiguration _config;

    public AuthController(IUserLogic userLogic, IConfiguration config)
    {
        _userLogic = userLogic;
        _config = config;
    }
    
    private List<Claim> GenerateClaims(User user) {  
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),    
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),      
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),      
            new Claim(ClaimTypes.Name, user.Username),
            new Claim("Email", user.Email),
            new Claim("Role", user.Role),
            new Claim("UserId", user.Id.ToString())
        };
        return claims.ToList(); }
    
    private string GenerateJwt(User user)
    {
        List<Claim> claims = GenerateClaims(user);
    
        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        SigningCredentials signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
    
        JwtHeader header = new JwtHeader(signIn);
    
        JwtPayload payload = new JwtPayload(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
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
            User user = await _userLogic.ValidateUser(dto.Username, dto.Password);
            string token = GenerateJwt(user);

            return Ok(token);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}