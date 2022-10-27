using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shared;
using Shared.Authorization;
using Shared.Dtos;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    
    private readonly ICompanyLogic CompanyLogic;
    private readonly IConfiguration Config;

    public AuthController(ICompanyLogic companyLogic, IConfiguration config)
    {
        CompanyLogic = companyLogic;
        Config = config;
    }
    
    private List<Claim> GenerateClaims(Company company) {  
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, Config["Jwt:Subject"]),    
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),      
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),      
            new Claim(ClaimTypes.Name, company.Username),
            new Claim("Email", company.Email),
            new Claim("SecurityLevel", company.SecurityLevel.ToString())
        };
        return claims.ToList(); }
    
    private string GenerateJwt(Company company)
    {
        List<Claim> claims = GenerateClaims(company);
    
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
    public async Task<ActionResult> Login([FromBody] CompanyLoginDto dto)
    {
        try
        {
            Company company = await CompanyLogic.ValidateCompany(dto.Username, dto.Password);
            string token = GenerateJwt(company);

            return Ok(token);

        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}