using System;
using System.ComponentModel.DataAnnotations;

namespace FarmlyNotificationsAPI.Models
{
    public class NotificationHubOptions
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ConnectionString { get; set; }
    }
}
