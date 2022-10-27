namespace Shared.Dtos;

public class EventCreationDto
{
    public string Username { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }
    
    public EventCreationDto(string username, string title, string description, DateTime dateTime)
    {
        Username = username;
        Title = title;
        Description = description;
        DateTime = dateTime;
    }
}