using Microsoft.AspNetCore.Mvc;

namespace RestApi.API.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthController : ControllerBase
{
    private readonly ILogger<HealthController> _logger;

    public HealthController(ILogger<HealthController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Get()
    {
        _logger.LogInformation("Health check requested at {Time}", DateTime.UtcNow);
        
        return Ok(new
        {
            status = "Healthy",
            timestamp = DateTime.UtcNow,
            service = "RestApi",
            version = "1.0.0"
        });
    }

    [HttpGet("ready")]
    public IActionResult Ready()
    {
        // Aqui você pode adicionar verificações mais complexas
        // Por exemplo, verificar se o banco de dados está acessível
        
        _logger.LogInformation("Readiness check requested at {Time}", DateTime.UtcNow);
        
        return Ok(new
        {
            status = "Ready",
            timestamp = DateTime.UtcNow
        });
    }

    [HttpGet("live")]
    public IActionResult Live()
    {
        _logger.LogInformation("Liveness check requested at {Time}", DateTime.UtcNow);
        
        return Ok(new
        {
            status = "Live",
            timestamp = DateTime.UtcNow
        });
    }
}
