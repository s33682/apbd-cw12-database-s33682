using DbFirst.DTOs;

namespace DbFirst.Services;

public interface IDbService
{
    public Task<IEnumerable<GetPatientDetailsDto>> GetPatients(string? search);
}