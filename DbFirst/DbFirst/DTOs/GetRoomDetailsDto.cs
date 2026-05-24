namespace DbFirst.DTOs;

public class GetRoomDetailsDto
{
    public string Id { get; set; } = String.Empty;
    public bool HasTv { get; set; } = false;
    public GetWardDetailsDto Ward { get; set; } = new();
}