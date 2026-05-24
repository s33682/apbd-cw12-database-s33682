using DbFirst.Models;

namespace DbFirst.Services;

public class DbService : IDbService
{
    private readonly HospitalContext _context;

    public DbService(HospitalContext context)
    {
        _context = context;
    }
}