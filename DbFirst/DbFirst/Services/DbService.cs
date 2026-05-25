using DbFirst.DTOs;
using DbFirst.Exceptions;
using DbFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DbFirst.Services;

public class DbService : IDbService
{
    private readonly HospitalContext _context;

    public DbService(HospitalContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GetPatientDetailsDto>> GetPatients(string? search)
    {
        IQueryable<Patient> data = _context.Patients
            .Include(p => p.BedAssignments)
            .ThenInclude(ba => ba.Bed)
            .ThenInclude(bed => bed.BedType)
            .Include(p => p.BedAssignments)
            .ThenInclude(ba => ba.Bed)
            .ThenInclude(bed => bed.Room)
            .Include(p => p.Admissions)
            .ThenInclude(ad => ad.Ward);

        if (search != null)
        {
            data = data.Where(p =>
                EF.Functions.Like(p.FirstName, $"%{search}%") || EF.Functions.Like(p.LastName, $"%{search}%"));
        }

        return await data.Select(p=> new GetPatientDetailsDto
        {
            Pesel =  p.Pesel,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Age = p.Age,
            Sex = p.Sex?"Male":"Female",
            Admissions = p.Admissions.Select(a => new GetAdmissionDetailsDto
            {
                Id = a.Id,
                AdmissionDate = a.AdmissionDate,
                DischargeDate = a.DischargeDate,
                Ward = new GetWardDetailsDto
                {
                    Id = a.Ward.Id,
                    Name = a.Ward.Name,
                    Description = a.Ward.Description
                }
            }).ToList(),
            BedAssigments = p.BedAssignments.Select(ba=> new GetBedAssigmentDetailsDto
            {
                Id = ba.Id,
                From = ba.From,
                To = ba.To,
                Bed = new GetBedDetailsDto
                {
                    Id = ba.Bed.Id,
                    BedType = new GetBedTypeDetailsDto
                    {
                        Id =  ba.Bed.BedType.Id,
                        Name = ba.Bed.BedType.Name,
                        Description = ba.Bed.BedType.Description
                    },
                    Room = new GetRoomDetailsDto
                    {
                        Id = ba.Bed.Room.Id,
                        HasTv =  ba.Bed.Room.HasTv,
                        Ward =  new GetWardDetailsDto
                        {
                            Id = ba.Bed.Room.Ward.Id,
                            Name = ba.Bed.Room.Ward.Name,
                            Description = ba.Bed.Room.Ward.Description
                        }
                    }
                }
            }).ToList()
        }).ToListAsync();
    }

    public async Task AssignBedForPatient(string pesel, PostBedAssigmentDto data)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Pesel == pesel);

        if (patient == null)
        {
            throw new PatientNotFoundException("Nie znaleziono pacjenta z podanym peselem!");
        }

        var options = await _context.Beds
            .Where( x=> x.BedType.Name == data.BedType && x.Room.Ward.Name == data.Ward)
            .Where(x=> x.BedAssignments.All(xba=>
                (xba.To != null && xba.To < data.From) ||
                (data.To != null && xba.From > data.To)
                ))
            .ToListAsync();

        if (options.Count == 0)
        {
            throw new NoBedsAvailableException("Nie znaleziono dostępnych łóżek w tym terminie!");
        }

        var newBedAssigment = new BedAssignment
        {
            PatientPesel = pesel,
            BedId = options[0].Id,
            From =  data.From,
            To = data.To
        };

        _context.BedAssignments.Add(newBedAssigment);
        await _context.SaveChangesAsync();
    }
}