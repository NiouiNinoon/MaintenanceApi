using MaintenanceApi.Models;
using MaintenanceApi.Dtos;

namespace MaintenanceApi.Services.Interfaces;
public interface IInterventionService
{
    Task<List<Intervention>> GetForTechnicianAsync(string technicianId);
    Task<Intervention> CreateAsync(CreateInterventionDto dto);
}
