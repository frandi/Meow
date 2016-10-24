using System;
using System.ComponentModel.DataAnnotations;

namespace Meow.Shared.DataAccess.Models
{
    public class Attendee: BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public Guid EventId { get; set; }
        public virtual EventItem Event { get; set; }
    }
}
