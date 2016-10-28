using Meow.Shared.DataAccess;
using Meow.Shared.DataAccess.Models;
using Meow.Shared.Infrastructure;
using System.Linq;
using System;
using Newtonsoft.Json;

namespace Meow.Domain.Event
{
    public class EventRepository: BaseRepository<EventItem>, IEventRepository
    {
        private const string EVENT_SINGLE_KEY_PREFIX = "event:single:";

        private CacheManager _cache;

        public EventRepository(MeowContext db, CacheManager cache)
            : base(db)
        {
            _cache = cache;
        }

        public override EventItem Get(Guid id)
        {
            EventItem item = null;

            string itemJson = _cache.Get(EVENT_SINGLE_KEY_PREFIX + id);
            if (!string.IsNullOrEmpty(itemJson))
                item = JsonConvert.DeserializeObject<EventItem>(itemJson);

            if (item == null)
            {
                item = base.Get(id);
                _cache.Set(EVENT_SINGLE_KEY_PREFIX + id, JsonConvert.SerializeObject(item));
            }

            return item;
        }
        
        public override void Update(EventItem entity)
        {
            base.Update(entity);

            _cache.Del(EVENT_SINGLE_KEY_PREFIX + entity.Id);
        }

        public override void Delete(EventItem entity)
        {
            base.Delete(entity);

            _cache.Del(EVENT_SINGLE_KEY_PREFIX + entity.Id);
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
