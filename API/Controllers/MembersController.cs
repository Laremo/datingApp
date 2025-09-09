using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")] //localhost:5001/api/members
[ApiController]
public class MembersController(AppDataContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
    {
        var members = await context.Users.ToListAsync();
        return members;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetMember(string id)
    {
        var user =  await context.Users.FindAsync(id);
        if (user == null) return NotFound();
        return user;
    }
}
