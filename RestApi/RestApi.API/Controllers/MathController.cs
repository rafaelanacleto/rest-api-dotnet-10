using Microsoft.AspNetCore.Mvc;
using RestApi.API.Services.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace RestApi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        private readonly IMathOperations _mathOperations;
        private readonly ILogger<MathController> _logger;
        public MathController(ILogger<MathController> logger, IMathOperations mathOperations)
        {
            _logger = logger;
            _mathOperations = mathOperations;
        }

        [HttpGet("sum/{firtsNum}/{secondNum}")]
        public IActionResult Get(string firtsNum, string secondNum)
        {
            if (IsNumeric(firtsNum) && IsNumeric(secondNum))
            {
                decimal total = _mathOperations.Sum(firtsNum, secondNum);
                return Ok(total);

            }
            return BadRequest();
        }

        [HttpGet("subtraction/{firtsNum}/{secondNum}")]
        public IActionResult Subtraction(string firtsNum, string secondNum)
        {
            if (IsNumeric(firtsNum) && IsNumeric(secondNum))
            {
                decimal total = _mathOperations.Subtract(firtsNum, secondNum);
                return Ok(total);
            }
            return BadRequest();
        }

        [HttpGet("multiplication/{firtsNum}/{secondNum}")]
        public IActionResult Multiplication(string firtsNum, string secondNum)
        {
            if (IsNumeric(firtsNum) && IsNumeric(secondNum))
            {
                decimal total = _mathOperations.Multiply(firtsNum, secondNum);
                return Ok(total);
            }
            return BadRequest();
        }

        [HttpGet("division/{firtsNum}/{secondNum}")]
        public IActionResult Division(string firtsNum, string secondNum)
        {
            if (IsNumeric(firtsNum) && IsNumeric(secondNum))
            {
                decimal total = _mathOperations.Divide(firtsNum, secondNum);
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
