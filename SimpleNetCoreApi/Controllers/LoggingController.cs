using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SimpleNetCoreApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Logging")]
    public class LoggingController : Controller
    {
        private readonly ILogger<LoggingController> _logger;

        public LoggingController(ILogger<LoggingController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("Information")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult LogInformation([FromBody] string message)
        {
            _logger.LogInformation(message);
            return Ok(message);
        }

        [HttpPost]
        [Route("Warning")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult LogWarning([FromBody] string message)
        {
            _logger.LogWarning(message);
            return Ok(message);
        }

        [HttpPost]
        [Route("Error")]
        [ProducesResponseType(typeof(string), 200)]
        public IActionResult LogError([FromBody] string message)
        {
            _logger.LogError(message);
            return Ok(message);
        }
    }
}