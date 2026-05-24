namespace DbFirst.DTOs;

public class GetAdmissionDetailsDto
{
    public int Id { get; set; }
    public DateTime AdmissionDate { get; set; }
    public DateTime? DischargeDate { get; set; } = null;
    public GetWardDetailsDto Ward { get; set; } = new();
}