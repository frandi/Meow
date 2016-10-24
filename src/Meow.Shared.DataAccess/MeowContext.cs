using Meow.Shared.DataAccess.Models;
using System.Data.Entity;

namespace Meow.Shared.DataAccess
{
    public class MeowContext: DbContext
    {
        public MeowContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<EventItem> Events { get; set; }
    }
}
