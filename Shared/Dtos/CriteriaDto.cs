namespace Shared.Dtos;

public class CriteriaDto
{
    public int OwnerId { get; init; }

    public string? Category { get; init; }
    
    public string? Area { get; init; }
    
    public CriteriaDto(int ownerId, string? category, string? area)
    {
        OwnerId = ownerId;
        Category = category;
        Area = area;
    }
    
    public CriteriaDto()
    {}
}