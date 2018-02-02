using Microsoft.AspNetCore.Mvc;

namespace SimpleNetCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Echo")]
    public class EchoController : Controller
    {
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult Get(int id)
        {
            return Ok(id);
        }

        [HttpPost]
        [ProducesResponseType(typeof(object), 200)]
        public IActionResult Post([FromBody]object o)
        {
            return Ok(o);
        }
    }
}