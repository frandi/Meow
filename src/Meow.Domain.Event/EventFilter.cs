using System;

namespace Meow.Domain.Event
{
    public class EventFilter
    {
        public Guid? OwnerId { get; set; }
        public Guid? AttendeeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
