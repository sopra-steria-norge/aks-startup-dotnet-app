using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AksStartupDotnetApp.Controllers
{
    [Authorize]
    [ApiController]
    public class TestAzureAdAuthentication : ControllerBase
    {

        public TestAzureAdAuthentication()
        {
        }

        [HttpGet("login")]
        public string Ping()
        {
            return "You are logged in!";
        }
    }
}
