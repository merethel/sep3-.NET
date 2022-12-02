namespace Shared.Dtos;

public class CriteriaDto
{
    public int OwnerId { get; set; }

    public string Category { get; set; }
    
    public string Area { get; set; }
    
    public CriteriaDto(int ownerId, string category, string area)
    {
        OwnerId = ownerId;
        Category = category;
        Area = area;
    }
    
    public CriteriaDto()
    {}
}