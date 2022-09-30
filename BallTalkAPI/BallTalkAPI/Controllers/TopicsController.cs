using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BallTalkAPI.Data;
using BallTalkAPI.Models;

namespace BallTalkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopicsController : ControllerBase
    {
        private readonly BallTalkContext _context;

        public TopicsController(BallTalkContext context)
        {
            _context = context;
        }

        // GET: api/Topics
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Topic>>> GetTopics()
        {
            return await _context.Topics.ToListAsync();
        }

        // GET: api/Topics/5
        [HttpGet("{name}")]
        public async Task<ActionResult<Topic>> GetTopic(string name)
        {
            var topic = await _context.Topics.FindAsync(name);

            if (topic == null)
            {
                return NotFound();
            }

            return topic;
        }

        // PUT: api/Topics/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}")]
        public async Task<IActionResult> PutTopic(string name, Topic topic)
        {
            if (name != topic.Name)
            {
                return BadRequest();
            }

            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Topics
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Topic>> PostTopic(Topic topic)
        {
            _context.Topics.Add(topic);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TopicExists(topic.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTopic", new { name = topic.Name }, topic);
        }

        // DELETE: api/Topics/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteTopic(string name)
        {
            var topic = await _context.Topics.FindAsync(name);
            if (topic == null)
            {
                return NotFound();
            }

            _context.Topics.Remove(topic);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TopicExists(string name)
        {
            return _context.Topics.Any(e => e.Name == name);
        }
    }
}
