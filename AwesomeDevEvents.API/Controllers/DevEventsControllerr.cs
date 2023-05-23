using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AwesomeDevEvents.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevEventsControllerr : ControllerBase
    {
        private readonly DevEventsDbContext _context;

        public DevEventsControllerr(DevEventsDbContext context)
        {
            _context = context;
        }

        // api/dev-events GET 
        [HttpGet]
        public IActionResult GetAll()
        {
            var DevEvents = _context.DevEvents.Where(d => !d.isDeleted).ToList();
            return Ok(DevEvents);
        }

        // api/dev-events/id GET 
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var DevEvents = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if(DevEvents == null)
            {
                return NotFound();
            }

            return Ok(DevEvents);
        }

        // api/dev-events/ POST 
        [HttpPost]
        public IActionResult Post(DevEvent devEvent)
        {
            _context.DevEvents.Add(devEvent);
            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);
        }

        // api/dev-events/id PUT
        [HttpPut("{id}")]
        public IActionResult Update(Guid id,DevEvent input)
        {
            var DevEvents = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (DevEvents == null)
            {
                return NotFound();
            }

            DevEvents.Update(input.Title, input.Description, input.StartDate, input.EndDate);
            return NoContent();
        }

        // api/dev-events/id DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var DevEvents = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (DevEvents == null)
            {
                return NotFound();
            }
            DevEvents.Delete();

            return NoContent();
        }

        // api/dev-events/id/speakers
        [HttpPost("{id}/speaker")]
        public IActionResult PostSpeaker(Guid id, DevEventSpeaker speaker)
        {
            var DevEvents = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (DevEvents == null)
            {
                return NotFound();
            }
            DevEvents.Speakers.Add(speaker);
            return Ok();
        }
    }

}
