namespace MaintenanceApi.Models;
public class Intervention
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime Date { get; set; }

    public Guid ClientId { get; set; }
    public Client? Client { get; set; }

    public Guid ServiceTypeId { get; set; }
    public ServiceType? ServiceType { get; set; }

    public ICollection<AppUser> Technicians { get; set; } = new List<AppUser>();
    public ICollection<Material> Materials { get; set; } = new List<Material>();
}
