using Microsoft.AspNetCore.Mvc;
using Bookstore.Services;  

namespace Bookstore.Presentation.Controllers;

[ApiController]
[Route("/system")]
public class SystemController : ControllerBase
{
    private readonly IDemoService _demoService;  

    public SystemController(IDemoService demoService)  
    {
        _demoService = demoService;
    }

    [HttpPost("reset")]
    public async Task<IActionResult> Reset()
    {
        await _demoService.ResetSystemToDemoAsync();
        return Ok(new { message = "Sistem temizlendi ve demo verileri yüklendi." });
    }

    [HttpPost("chaos")]
    public async Task<IActionResult> Chaos([FromQuery] int count = 50)
    {
        await _demoService.CreateTestingChaosAsync(count);
        return Ok(new { message = $"{count} adet rastgele bozuk veri eklendi." });
    }
}