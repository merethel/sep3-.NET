namespace Shared.Models;

public class Event
{
    public int Id { get; set; }
    public User Owner { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    
    public string Location { get; set; }
    public DateTime DateTime { get; set; }
    
    public List<User> Attendees { get; set; }

    public Event(User owner, string title, string description, string location, DateTime dateTime, List<User> attendees)
    {
        Owner = owner;
        Title = title;
        Description = description;
        Location = location;
        DateTime = dateTime;
        Attendees = attendees;
    }

    public Event()
    {
        
    }
}
