using MaintenanceApi.Data;
using MaintenanceApi.Dtos;
using MaintenanceApi.Models;
using MaintenanceApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MaintenanceApi.Repositories;

public class InterventionRepository : IInterventionRepository
{
    private readonly AppDbContext _context;

    public InterventionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Intervention> CreateAsync(CreateInterventionDto dto)
    {
        var technicians = await _context.Users.Where(u => dto.TechnicianIds.Contains(u.Id)).ToListAsync();
        var materials = await _context.Materials.Where(m => dto.MaterialIds.Contains(m.Id)).ToListAsync();

        var intervention = new Intervention
        {
            ClientId = dto.ClientId,
            ServiceTypeId = dto.ServiceTypeId,
            Date = dto.Date,
            Technicians = technicians,
            Materials = materials
        };

        _context.Interventions.Add(intervention);
        await _context.SaveChangesAsync();
        return intervention;
    }

    public async Task<List<Intervention>> GetByTechnicianIdAsync(string technicianId)
    {
        return await _context.Interventions
            .Where(i => i.Technicians.Any(t => t.Id == technicianId))
            .Include(i => i.Client)
            .Include(i => i.Materials)
            .Include(i => i.ServiceType)
            .ToListAsync();
    }
}
