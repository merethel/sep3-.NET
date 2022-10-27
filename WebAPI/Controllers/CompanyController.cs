using Application.LogicInterfaces;
using Shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyLogic CompanyLogic;
    private readonly IConfiguration Config;

    public CompanyController(ICompanyLogic companyLogic, IConfiguration config)
    {
        CompanyLogic = companyLogic;
        Config = config;
    }

    [HttpPost]
    public async Task<ActionResult<Company>> CreateAsync(CompanyCreationDto dto)
    {
        try
        {
            Company company = await CompanyLogic.CreateAsync(dto);
            return Created($"/users/{company.Id}", company);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}