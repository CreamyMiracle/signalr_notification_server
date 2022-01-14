using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace NotificationServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HubsController : ControllerBase
    {
        public HubsController(ILogger<HubsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<string>> GetHubs()
        {
            return new List<string>() { Constants.NotificationHub };
        }

        #region Private Fields

        private readonly ILogger<HubsController> _logger;
        #endregion
    }
}