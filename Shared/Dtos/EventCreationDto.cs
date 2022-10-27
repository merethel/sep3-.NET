namespace Shared.Dtos;

public class EventCreationDto
{
    public int CompanyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }
    
    public EventCreationDto(int ownerId, string title, string description, DateTime dateTime)
    {
        ownerId = ownerId;
        Title = title;
        Description = description;
        DateTime = dateTime;
    }
}