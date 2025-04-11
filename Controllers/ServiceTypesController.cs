using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MaintenanceApi.Data;
using MaintenanceApi.Models;

namespace MaintenanceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ServiceTypesController : ControllerBase
{
    private readonly AppDbContext _context;
    public ServiceTypesController(AppDbContext context) => _context = context;

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.ServiceTypes.ToList());

    [HttpPost]
    public async Task<IActionResult> Create(ServiceType type)
    {
        _context.ServiceTypes.Add(type);
        await _context.SaveChangesAsync();
        return Ok(type);
    }
}
