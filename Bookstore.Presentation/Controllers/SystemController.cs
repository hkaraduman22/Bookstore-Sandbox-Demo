using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bookstore.Services;

namespace Bookstore.Presentation.Controllers;

[Authorize(Roles = "ADMIN")]
[ApiController]
[Route("api/system")]
public class SystemController : ControllerBase
{
    private readonly IDemoService _demoService;

    public SystemController(IDemoService demoService)
    {
        _demoService = demoService;
    }

    [HttpPost("restore")]
    public async Task<IActionResult> Restore()
    {
        await _demoService.ResetSystemToDemoAsync();
        return Ok();
    }
}