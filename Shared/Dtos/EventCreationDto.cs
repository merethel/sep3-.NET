namespace Shared.Dtos;

public class EventCreationDto
{
    public string Username { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public string Location { get; set; }
    public DateTime DateTime { get; set; }
    public string Category { get; set; }
    public string Area { get; set; }
    
    
    public EventCreationDto(string username, string title, string description, string location,DateTime dateTime)
    {
        Username = username;
        Title = title;
        Description = description;
        Location = location;
        DateTime = dateTime;
    }

    public EventCreationDto()
    {
    }
}