using Meow.Shared.DataAccess;
using Meow.Shared.DataAccess.Models;
using Meow.Shared.Infrastructure;
using System.Linq;

namespace Meow.Domain.Event
{
    public class EventRepository: BaseRepository<EventItem>, IEventRepository
    {
        public EventRepository(MeowContext db)
            : base(db)
        {

        }

        public IQueryable<EventItem> SearchEvents(EventFilter filter)
        {
            var items = Filter(e =>
                (filter.OwnerId == null || e.OwnerId == filter.OwnerId.Value)
                && (filter.StartDate == null || filter.StartDate.Value <= e.EndTime)
                && (filter.EndDate == null || filter.EndDate.Value >= e.StartTime)
            );

            return items;
        }
    }
}
