using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MaintenanceApi.Data;
using MaintenanceApi.Models;

namespace MaintenanceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class MaterialsController : ControllerBase
{
    private readonly AppDbContext _context;
    public MaterialsController(AppDbContext context) => _context = context;

    [HttpGet]
    public IActionResult GetAll() => Ok(_context.Materials.ToList());

    [HttpPost]
    public async Task<IActionResult> Create(Material material)
    {
        _context.Materials.Add(material);
        await _context.SaveChangesAsync();
        return Ok(material);
    }
}
