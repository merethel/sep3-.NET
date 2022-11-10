namespace Shared.Models;

public class Event
{
    public int Id { get; set; }
    public User Owner { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public string Location { get; set; }
    public DateTime DateTime { get; set; }
    
    public Event(int id, User owner, string title, string description, string location, DateTime dateTime)
    {
        Owner = owner;
        Title = title;
        Description = description;
        Location = location;
        DateTime = dateTime;
        Id = id;
    }

    public Event()
    {
        
    }
}
