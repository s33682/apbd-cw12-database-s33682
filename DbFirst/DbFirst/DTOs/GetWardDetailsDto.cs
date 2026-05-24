namespace DbFirst.DTOs;

public class GetWardDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Description { get; set; } = String.Empty;
}