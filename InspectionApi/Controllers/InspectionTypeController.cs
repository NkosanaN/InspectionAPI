using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InspectionApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionTypeController : ControllerBase
    {
        private readonly DataContext _context;

        public InspectionTypeController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InspectionType>>> GetInspectionType()
        {
            return await _context.InspectionTypes.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InspectionType>> GetInspectionType(int id)
        {
            var inspectiontypes = await _context.InspectionTypes.FindAsync(id);

            if (inspectiontypes == null)
                return NotFound();

            return Ok(inspectiontypes);
        }

        [HttpPost]
        public async Task<ActionResult<InspectionType>> PostInspectionType(InspectionType inspectiontype)
        {
            _context.InspectionTypes.Add(inspectiontype);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetInspectionType", new { id = inspectiontype.Id }, inspectiontype);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutInspectionType(int id, InspectionType inspectiontype)
        {
            if (id != inspectiontype.Id)
            {
                return BadRequest();
            }
            _context.Entry(inspectiontype).State = EntityState.Modified;

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
        // GET: InspectionTypeController/Delete/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var type = await _context.InspectionTypes.FindAsync(id);
            if (type == null)
                return NotFound();

            _context.InspectionTypes.Remove(type);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
