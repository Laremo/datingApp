using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/[controller]")] //localhost:5001/api/members
[ApiController]
public class MembersController(AppDataContext context) : ControllerBase
{
    [HttpGet]
    public ActionResult<IReadOnlyList<AppUser>> GetMembers()
    {
        var members = context.Users.ToList();
        return members;
    }

    [HttpGet("{id}")]
    public ActionResult<AppUser> GetMember(string id)
    {
        var user = context.Users.Find(id);
        if (user == null) return NotFound();
        return user;
    }
}
