namespace Shared.Dtos;

public class RegisterAttendeeDto
{
    public int UserId { get;}
    public int EventId { get;}

    public RegisterAttendeeDto(int userId, int eventId)
    {
        UserId = userId;
        EventId = eventId;
    }
}