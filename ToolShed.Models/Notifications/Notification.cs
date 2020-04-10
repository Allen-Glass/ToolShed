using System.ComponentModel.DataAnnotations;
using ToolShed.Models.API;
using ToolShed.Models.Enums;

namespace ToolShed.Models.Notifications
{
    public abstract class Notification
    {
        [Required]
        public User User { get; set; }

        public string Body { get; set; }

        public NotificationType NotificationType { get; set; }
    }
}
