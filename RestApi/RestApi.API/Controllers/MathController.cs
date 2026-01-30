using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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
                var total = ConvertToLong(firtsNum) + ConvertToLong(secondNum);
                return Ok();

            }
            return BadRequest();
        }

        private int ConvertToLong(string num)
        {
            throw new NotImplementedException();
        }

        private bool IsNumeric(string num)
        {
            throw new NotImplementedException();
        }
    }

}
