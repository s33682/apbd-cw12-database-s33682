using System.ComponentModel.DataAnnotations;

namespace DbFirst.DTOs;

public class PostBedAssigmentDto
{
    [Required]
    public DateTime From { get; set; }
    public DateTime? To { get; set; } = null;
    [Required]
    public string BedType { get; set; } = "";
    [Required]
    public string Ward { get; set; } = "";
}