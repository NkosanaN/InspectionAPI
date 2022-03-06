using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspectionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly DataContext _context;
        public StatusesController(DataContext context)
        {
            _context = context;
        }
        // GET: api/<StatusesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
            return await _context.Statuses.ToListAsync();
        }

        // GET api/<StatusesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatuses(int id)
        {
            var stautues = await _context.Statuses.FindAsync(id);

            if (stautues == null)
                return NotFound();

            return Ok(stautues);
        }
        // POST api/<StatusesController>
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatuses(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetStatuses", new { id = status.Id }, status);
        }

        // PUT api/<StatusesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutStatus(int id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }
            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                //if(!InspecitionExist(id))
                throw;
            }

            return NoContent();
        }

        // DELETE api/<StatusesController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStatues(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
                return NotFound();

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool StatuesExist(int id)
        {
            return _context.Statuses.Any(x => x.Id == id);
        }
    }
}