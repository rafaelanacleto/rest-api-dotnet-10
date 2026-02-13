using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using RestApi.API.Utils;

namespace RestApi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        private readonly ILogger<MathController> _logger;
        public MathController(ILogger<MathController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firtsNum}/{secondNum}")]
        public IActionResult Get(string firtsNum, string secondNum)
        {
            if (IsNumeric(firtsNum) && IsNumeric(secondNum))
            {
                decimal total = MathRest.Sum(firtsNum, secondNum);
                return Ok(total);

            }
            return BadRequest();
        }

        [HttpGet("subtraction/{firtsNum}/{secondNum}")]
        public IActionResult Subtraction(string firtsNum, string secondNum)
        {
            if (IsNumeric(firtsNum) && IsNumeric(secondNum))
            {
                decimal total = MathRest.Subtract(firtsNum, secondNum);
                return Ok(total);
            }
            return BadRequest();
        }

        [HttpGet("multiplication/{firtsNum}/{secondNum}")]
        public IActionResult Multiplication(string firtsNum, string secondNum)
        {
            if (IsNumeric(firtsNum) && IsNumeric(secondNum))
            {
                decimal total = MathRest.Multiply(firtsNum, secondNum);
                return Ok(total);
            }
            return BadRequest();
        }

        [HttpGet("division/{firtsNum}/{secondNum}")]
        public IActionResult Division(string firtsNum, string secondNum)
        {
            if (IsNumeric(firtsNum) && IsNumeric(secondNum))
            {
                decimal total = MathRest.Divide(firtsNum, secondNum);
                return Ok(total);
            }
            return BadRequest();
        }

        private bool IsNumeric(string num)
        {
            return decimal.TryParse(num, out _);
        }
    }

}
