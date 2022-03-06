using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspectionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionController : ControllerBase
    {
        private readonly DataContext _context;

        public InspectionController(DataContext context)
        {
            _context = context;
        }

        // GET: api/<InspectionController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Inspection>>> GetInspections()
        {
            return await _context.Inspections.ToListAsync();
        }

        // GET api/<InspectionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Inspection>> GetInspection(int id)
        {
            var inspection = await _context.Inspections.FindAsync(id);

            if (inspection == null)
                return NotFound();

            return Ok(inspection);
        }

        // POST api/<InspectionController>
        [HttpPost]
        public async Task<ActionResult<Inspection>> PostInspection(Inspection inspection)
        {
            _context.Inspections.Add(inspection);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetInspection", new { id = inspection.Id }, inspection);
        }

        // PUT api/<InspectionController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutInspection(int id, Inspection inspection)
        {
            if (id != inspection.Id)
            {
                return BadRequest();
            }
            _context.Entry(inspection).State = EntityState.Modified;

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

        // DELETE api/<InspectionController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInspection(int id)
        {
            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection == null)
                return NotFound();

            _context.Inspections.Remove(inspection);
            await _context.SaveChangesAsync();


            return NoContent();
        }

        private bool InspecitionExist(int id)
        {
            return _context.Inspections.Any(x => x.Id == id);
        }
    }
}

