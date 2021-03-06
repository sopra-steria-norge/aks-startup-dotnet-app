using System;
using Microsoft.AspNetCore.Mvc;

namespace AksStartupDotnetApp.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        public HealthController()
        {
        }

        /// <summary>
        /// Health endpoint to check if the service is running. Required for the API gateway to work.
        /// </summary>
        /// <returns></returns>
        [HttpGet("health")]
        public string Health()
        {
            return "OK";
        }

        [HttpGet("/")]
        public string HealthRoot()
        {
            return "OK";
        }

        [HttpGet("test")]
        public string HealthRoot1234()
        {
            return "123";
        }

        /// <summary>
        /// Health endpoint to check if the service is able to connect to the database and read values.
        /// </summary>
        /// <returns></returns>
        // [HttpGet("TestDatabaseConnection")]
        // public string TestDatabaseConnection()
        // {
        //     if (_aksStartupDotnetAppDbService.CanConnectToDatabase())
        //     {
        //         return "OK";
        //     }
        //     throw new Exception("Could not connect to the database");
        // }

    }
}
