namespace SeLoger.Api.Streaming.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System;

    [Produces("application/json")]
    [Route("api/healthcheck")]
    [ApiVersionNeutral]
    public class OrderController : ControllerBase
    {
        /// <summary>
        /// HealthCheck c'est cool!
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get(Guid id)
        {
            return Content("OK");
        }
    }
}
