using Application.LogicInterfaces;
using GrpcService1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Dtos;
using Shared.Models;

namespace WebAPI.Controllers;


[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventLogic _eventLogic;

    public EventController(IEventLogic eventLogic)
    {
        _eventLogic = eventLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Event>> CreateAsync(EventCreationDto dto)
    {
        try
        {
            Event @event = await _eventLogic.CreateAsync(dto);
            return Created($"/users/{@event.Id}", @event);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }


    [HttpGet]
    public async Task<ActionResult<List<Event>>> GetAsync([FromQuery] int ownerId, [FromQuery] string? area, [FromQuery] string? category, [FromQuery] bool? isCancelled, [FromQuery] int? attendee)
    {
        try
        {
            List<Event> events = await _eventLogic.GetAsync(new CriteriaDto()
            {
                OwnerId = ownerId,
                Area = area,
                Attendee = attendee,
                IsCancelled = isCancelled,
                Category = category
            });
            return events;
        }
        
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPut]
    public async Task<ActionResult<Event>> RegisterAttendeeAsync(RegisterAttendeeDto dto)
    {
        try
        {
            Event eventToReturn = await _eventLogic.RegisterAttendeeAsync(dto.UserId, dto.EventId);
            return eventToReturn;
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<Event>> CancelAsync(IntRequest eventId)
    {
        try
        {
            Event @event = await _eventLogic.CancelAsync(eventId.Int);
            return @event;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}