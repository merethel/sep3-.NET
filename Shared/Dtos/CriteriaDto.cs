namespace Shared.Dtos;

public class CriteriaDto
{
    public int OwnerId { get; init; }

    public string? Category { get; set; }
    
    public string? Area { get; set; }
    
    public bool? IsCancelled { get; set; }
    
    public int? Attendee { get; set; }
    
    
    public CriteriaDto(int ownerId, string? category, string? area)
    {
        OwnerId = ownerId;
        Category = category;
        Area = area;
    }
    
    public CriteriaDto()
    {
    }
}