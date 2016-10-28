using Meow.Shared.DataAccess.Models;
using Meow.Shared.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;

namespace Meow.Domain.Event
{
    public class EventService : IEventService
    {
        private IEventRepository _repo;

        public EventService(IEventRepository repo)
        {
            if (repo == null)
                throw new ArgumentNullException(nameof(repo));

            _repo = repo;
        }

        public EventItem GetEvent(Guid id)
        {
            return _repo.Get(id);
        }

        public IEnumerable<EventItem> GetEventsByAttendee(Guid attendeeId)
        {
            if (attendeeId.Equals(Guid.Empty))
                throw new ArgumentNullException(nameof(attendeeId));

            var filter = new EventFilter() { AttendeeId = attendeeId };
            return _repo.SearchEvents(filter);
        }

        public IEnumerable<EventItem> GetEventsByOwner(Guid ownerId)
        {
            //if (ownerId.Equals(Guid.Empty))
            //    throw new ArgumentNullException(nameof(ownerId));

            var filter = new EventFilter() { OwnerId = ownerId };
            return _repo.SearchEvents(filter);
        }

        public IEnumerable<EventItem> GetEventsByDateRange(DateTime startDate, DateTime? endDate)
        {
            var filter = new EventFilter() { StartDate = startDate, EndDate = endDate };
            return _repo.SearchEvents(filter);
        }

        public EventItem CreateEvent(EventItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            _repo.Add(item);
            
            return (EventItem)item.Clone();
        }
        
        public EventItem UpdateEvent(EventItem item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var exisitingItem = _repo.Get(item.Id);

            if (exisitingItem == null)
                throw new EntityNotFoundException();

            exisitingItem.Name = item.Name;
            exisitingItem.OwnerId = item.OwnerId;
            exisitingItem.Location = item.Location;
            exisitingItem.StartTime = item.StartTime;
            exisitingItem.EndTime = item.EndTime;

            _repo.Update(exisitingItem);
            
            return (EventItem)exisitingItem.Clone();
        }

        public void DeleteEvent(Guid id)
        {
            if (id.Equals(Guid.Empty))
                throw new ArgumentNullException(nameof(id));

            var item = GetEvent(id);
            if (item != null)
            {
                _repo.Delete(item);
            }
        }
    }
}
