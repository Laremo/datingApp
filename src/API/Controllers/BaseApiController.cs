using Microsoft.AspNetCore.Mvc;
using API.Helpers;

namespace API.Controllers;

[ServiceFilter(typeof(UserActivityLogger))]
[Route("api/[controller]")]
[ApiController]
public class BaseApiController : ControllerBase
{
}
