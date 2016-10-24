using System;
using System.ComponentModel.DataAnnotations;

namespace Meow.Shared.DataAccess.Models
{
    public class EventItem: BaseModel
    {
        [Required]
        public string Name { get; set; }
        public Guid OwnerId { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
