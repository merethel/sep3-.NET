namespace Shared.Dtos;

public class EventCreationDto
{
    public int CompanyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DateTime { get; set; }
    
    public EventCreationDto(int companyId, string title, string description, DateTime dateTime)
    {
        CompanyId = companyId;
        Title = title;
        Description = description;
        DateTime = dateTime;
    }
}