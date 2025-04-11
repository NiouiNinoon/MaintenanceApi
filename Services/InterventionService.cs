using MaintenanceApi.Data;
using MaintenanceApi.Dtos;
using MaintenanceApi.Models;
using Microsoft.EntityFrameworkCore;
using MaintenanceApi.Services.Interfaces;

namespace MaintenanceApi.Services;
public class InterventionService : IInterventionService
{
    private readonly AppDbContext _context;

    public InterventionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Intervention> CreateAsync(CreateInterventionDto dto)
    {
        if (dto.Date < DateTime.UtcNow)
            throw new Exception("La date ne peut pas être dans le passé");

        var intervention = new Intervention
        {
            ClientId = dto.ClientId,
            ServiceTypeId = dto.ServiceTypeId,
            Date = dto.Date,
            Materials = await _context.Materials.Where(m => dto.MaterialIds.Contains(m.Id)).ToListAsync(),
            Technicians = await _context.Users.Where(u => dto.TechnicianIds.Contains(u.Id)).ToListAsync()
        };

        _context.Interventions.Add(intervention);
        await _context.SaveChangesAsync();
        return intervention;
    }

    public async Task<List<Intervention>> GetForTechnicianAsync(string technicianId)
    {
        return await _context.Interventions
            .Where(i => i.Technicians.Any(t => t.Id == technicianId))
            .Include(i => i.Client)
            .Include(i => i.Materials)
            .ToListAsync();
    }
}
