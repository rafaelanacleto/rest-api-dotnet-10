using Microsoft.AspNetCore.Mvc;
using RestApi.API.Model;

namespace RestApi.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreetingController : ControllerBase
    {

        private static long _counter = 0;
        private static readonly string _template = "Hello {0}";


        [HttpGet]
        public Greeting Index([FromQuery] string name = "")
        {
            var id = _counter++;
            var content = string.Format(_template, name);
            return new Greeting(id, content);
        }

    }
}
