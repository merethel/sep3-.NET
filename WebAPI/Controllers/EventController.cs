using Application.LogicInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Dtos;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventLogic EventLogic;

    public EventController(IEventLogic eventLogic)
    {
        EventLogic = eventLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Event>> CreateAsync(EventCreationDto dto)
    {
        try
        {
            Event @event = await EventLogic.CreateAsync(dto);
            return Created($"/users/{@event.Id}", @event);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }}