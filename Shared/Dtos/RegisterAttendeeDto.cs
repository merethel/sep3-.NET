namespace Shared.Dtos;

public class RegisterAttendeeDto
{
    public int UserId { get; set; }
    public int EventId { get; set; }

    public RegisterAttendeeDto(int userId, int eventId)
    {
        UserId = userId;
        EventId = eventId;
    }
}