using EventAppBackend_RestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EventAppBackend_RestAPI.Controllers
{
    [Route("api/Authorize")]
    [ApiController]
    public class AuthorizeController : ControllerBase
    {
        private readonly IConfiguration config;

        public AuthorizeController(IConfiguration config)
        {
            this.config = config;
        }

        [HttpPost]
        public ActionResult<string> Authorize(AuthorizeRequest req)
        {
            var ocPassword = config.GetValue<string>("OCPassword");

            if (req.OcPassword == ocPassword)
            {
                return Ok("\"Authorized\"");
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
