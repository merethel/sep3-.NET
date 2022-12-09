namespace Shared.Models;

public class Event
{
    public int Id { get; set; }
    public User Owner { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

    public string Location { get; set; } = null!;
    public DateTime DateTime { get; set; }
    
    public string Category { get; set; } = null!;
    public string Area { get; set; } = null!;
    public List<User> Attendees { get; set; } = null!;

    public Event(User owner, string title, string description, string location, DateTime dateTime, string category, string area, List<User> attendees)
    {
        Owner = owner;
        Title = title;
        Description = description;
        Location = location;
        DateTime = dateTime;
        Category = category;
        Area = area;
        Attendees = attendees;
    }

    public Event()
    {
        
    }

    public override string ToString()
    {
        return "Owner: " + Owner.Username + "Title: " + Title;
    }
}
