using Shared;
using Shared.Models;

namespace FileData;

public class DataContainer
{
    public ICollection<User> Companies { get; set; }
    public ICollection<Event> Events { get; set; }

}