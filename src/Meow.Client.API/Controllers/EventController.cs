using Meow.Domain.Event;
using Meow.Shared.DataAccess.Models;
using Meow.Shared.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meow.Client.API.Controllers
{
    [Route("api/[controller]")]
    public class EventController: Controller
    {
        private IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<EventItem> GetEvents()
        {
            return _service.GetEventsByOwner(Guid.Empty);
        }

        [HttpGet("{id}", Name = "GetEvent")]
        public EventItem GetEvent(Guid id)
        {
            return _service.GetEvent(id);
        }

        [HttpPost]
        public IActionResult CreateEvent([FromBody]EventItem value)
        {
            var item =_service.CreateEvent(value);
            return CreatedAtRoute("GetEvent", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(Guid id, [FromBody]EventItem item)
        {
            if (item == null || item.Id != id)
                return BadRequest();
            
            try
            {
                _service.UpdateEvent(item);

                return new NoContentResult();
            }
            catch (EntityNotFoundException)
            {
                return new NotFoundResult();
            }
        }

        public IActionResult DeleteEvent(Guid id)
        {
            _service.DeleteEvent(id);

            return new NoContentResult();
        }
    }
}
