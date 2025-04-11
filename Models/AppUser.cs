using Microsoft.AspNetCore.Identity;

namespace MaintenanceApi.Models;
public class AppUser : IdentityUser
{
    public ICollection<Intervention>? Interventions { get; set; }
}
