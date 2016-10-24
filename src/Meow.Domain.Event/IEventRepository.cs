using Meow.Shared.DataAccess.Models;
using Meow.Shared.Infrastructure;
using System.Linq;

namespace Meow.Domain.Event
{
    public interface IEventRepository: IBaseRepository<EventItem>
    {
        IQueryable<EventItem> SearchEvents(EventFilter filter);
    }
}
