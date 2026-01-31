using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;

namespace RestApi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        [HttpGet("sum/{firtsNum}/{secondNum}")]
        public IActionResult Get(string firtsNum, string secondNum)
        {
            if (IsNumeric(firtsNum) && IsNumeric(secondNum))
            {
                decimal total = ConvertToLong(firtsNum) + ConvertToLong(secondNum);
                return Ok(total);

            }
            return BadRequest();
        }

        private decimal ConvertToLong(string num)
        {
            return decimal.Parse(num);
        }

        private bool IsNumeric(string num)
        {
            return decimal.TryParse(num, out _);
        }
    }

}
