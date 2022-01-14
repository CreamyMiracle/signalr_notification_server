using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NotificationServer.Hubs;
using NotificationServer.Models;
using System.ComponentModel.DataAnnotations;

namespace NotificationServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        public NotificationsController(ILogger<NotificationsController> logger, IHubContext<NotificationHub> msgHubContext, Dictionary<string, List<Payload>> messages)
        {
            _logger = logger;
            _notificationHubContext = msgHubContext;
            Messages = messages;
        }

        [HttpGet("{group}")]
        public async Task<IEnumerable<Payload>> GetMessages(string group)
        {
            return Messages.ContainsKey(group) ? Messages[group] : new List<Payload>();
        }

        [HttpPost("{group}")]
        public async Task<Payload> SendMessage([Required] string group, [Required] Payload msg)
        {
            await _notificationHubContext.Clients.Group(group).SendAsync("client_side_method_identifier", msg);

            if (Messages.ContainsKey(group))
            {
                Messages[group].Add(msg);
            }
            else
            {
                Messages.Add(group, new List<Payload>() { msg });
            }

            return msg;
        }


        #region Private Fields
        private Dictionary<string, List<Payload>> Messages = new Dictionary<string, List<Payload>>();

        private readonly ILogger<NotificationsController> _logger;

        private readonly IHubContext<NotificationHub> _notificationHubContext;
        #endregion
    }
}