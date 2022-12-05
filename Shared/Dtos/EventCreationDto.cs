namespace Shared.Dtos;

public class EventCreationDto
{
    public string Username { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;
    public DateTime DateTime { get; set; }
    public string Category { get; set; } = null!;
    public string Area { get; set; } = null!;


    public EventCreationDto(string username, string title, string description, string location, DateTime dateTime, string category, string area)
    {
        Username = username;
        Title = title;
        Description = description;
        Location = location;
        DateTime = dateTime;
        Category = category;
        Area = area;
    }

    public EventCreationDto()
    {
    }
}