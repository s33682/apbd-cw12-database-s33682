namespace DbFirst.DTOs;

public class GetPatientDetailsDto
{
    public string Pesel { get; set; } = String.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public int Age { get; set; }
    public string Sex  { get; set; } =  String.Empty;
    public List<GetAdmissionDetailsDto> Admissions { get; set; } = [];
    public List<GetBedAssigmentDetailsDto> BedAssigments { get; set; } = [];
}