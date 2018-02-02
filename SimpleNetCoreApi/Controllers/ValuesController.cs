using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace SimpleNetCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(string[]), 200)]
        public IEnumerable<string> Get()
        {
            return new[] { "value1", "value2" };
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(string), 200)]
        public string Get(int id)
        {
            return "value-" + id;
        }
    }
}
