using GigHub.Models;
using System;

namespace GigHub.Controllers.Api
{
    public class NotificationDto
    {

        public DateTime DateTime { get; private set; }

        public NotificationType Type { get; private set; }

        public DateTime? OriginalDateTime { get; private set; }

        public string OriginalVenue { get; private set; }

        public Gig Gig { get; private set; }
    }
}