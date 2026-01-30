using Microsoft.AspNetCore.Mvc;

namespace RestApi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index(string firtsNum, string secondNum)
        {
            
        }
    }
}
