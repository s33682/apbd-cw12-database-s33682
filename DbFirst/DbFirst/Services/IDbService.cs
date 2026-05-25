using DbFirst.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DbFirst.Services;

public interface IDbService
{
    public Task<IEnumerable<GetPatientDetailsDto>> GetPatients(string? search);
    public Task AssignBedForPatient(string  pesel, PostBedAssigmentDto data);
}