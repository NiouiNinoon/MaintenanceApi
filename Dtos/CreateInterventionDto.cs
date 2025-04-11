namespace MaintenanceApi.Dtos;
public class CreateInterventionDto
{
    public Guid ClientId { get; set; }
    public Guid ServiceTypeId { get; set; }
    public DateTime Date { get; set; }
    public List<string> TechnicianIds { get; set; } = new();
    public List<Guid> MaterialIds { get; set; } = new();
}
