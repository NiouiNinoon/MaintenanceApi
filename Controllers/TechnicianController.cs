using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MaintenanceApi.Dtos;
using MaintenanceApi.Models;

namespace MaintenanceApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class TechniciansController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;


    public TechniciansController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTechnicianDto dto)
    {
        var user = new AppUser { UserName = dto.Username };
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        await _userManager.AddToRoleAsync(user, "Technician");

        return Ok(new { user.Id, user.UserName });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        // Pas idéal de tout exposer, à restreindre selon les besoins
        return Ok(_userManager.Users.ToList());
    }
}
