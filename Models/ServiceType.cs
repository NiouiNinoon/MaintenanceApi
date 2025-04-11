namespace MaintenanceApi.Models;
public class ServiceType
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
}
