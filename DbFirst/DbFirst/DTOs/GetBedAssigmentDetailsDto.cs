namespace DbFirst.DTOs;

public class GetBedAssigmentDetailsDto
{
    public int Id { get; set; }
    public DateTime From { get; set; }
    public DateTime? To { get; set; } = null;
    public GetBedDetailsDto Bed { get; set; } = new();
}