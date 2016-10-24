using Meow.Shared.DataAccess.Models;
using System;
using System.Collections.Generic;

namespace Meow.Domain.Event
{
    public interface IEventService
    {
        EventItem GetEvent(Guid id);
        IEnumerable<EventItem> GetEventsByOwner(Guid ownerId);
        IEnumerable<EventItem> GetEventsByAttendee(Guid attendeeId);
        IEnumerable<EventItem> GetEventsByDateRange(DateTime startDate, DateTime? endDate);
        EventItem CreateEvent(EventItem item);
        EventItem UpdateEvent(EventItem item);
        void DeleteEvent(Guid id);
    }
}
