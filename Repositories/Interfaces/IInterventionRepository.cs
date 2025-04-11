using MaintenanceApi.Dtos;
using MaintenanceApi.Models;

namespace MaintenanceApi.Repositories.Interfaces;

public interface IInterventionRepository
{
    Task<Intervention> CreateAsync(CreateInterventionDto dto);
    Task<List<Intervention>> GetByTechnicianIdAsync(string technicianId);
}
