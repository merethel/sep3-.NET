using Shared;

namespace FileData;

public class DataContainer
{
    public ICollection<Company> Companies { get; set; }
    public ICollection<Event> Events { get; set; }

}