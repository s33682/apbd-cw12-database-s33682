namespace DbFirst.DTOs;

public class GetBedDetailsDto
{
    public int  Id { get; set; }
    public GetBedTypeDetailsDto BedType { get; set; } = new();
    public GetRoomDetailsDto Room { get; set; } = new();
}