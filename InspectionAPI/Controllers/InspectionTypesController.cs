using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InspectionAPI;
using InspectionAPI.Data;

namespace InspectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public InspectionTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/InspectionTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InspectionType>>> GetInspectionTypes()
        {
            return await _context.InspectionTypes.ToListAsync();
        }

        // GET: api/InspectionTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InspectionType>> GetInspectionType(int id)
        {
            var inspectionType = await _context.InspectionTypes.FindAsync(id);

            if (inspectionType == null)
            {
                return NotFound();
            }

            return inspectionType;
        }

        // PUT: api/InspectionTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInspectionType(int id, InspectionType inspectionType)
        {
            if (id != inspectionType.Id)
            {
                return BadRequest();
            }

            _context.Entry(inspectionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionTypeExists(id))
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

        // POST: api/InspectionTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<InspectionType>> PostInspectionType(InspectionType inspectionType)
        {
            _context.InspectionTypes.Add(inspectionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInspectionType", new { id = inspectionType.Id }, inspectionType);
        }

        // DELETE: api/InspectionTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInspectionType(int id)
        {
            var inspectionType = await _context.InspectionTypes.FindAsync(id);
            if (inspectionType == null)
            {
                return NotFound();
            }

            _context.InspectionTypes.Remove(inspectionType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InspectionTypeExists(int id)
        {
            return _context.InspectionTypes.Any(e => e.Id == id);
        }
    }
}
