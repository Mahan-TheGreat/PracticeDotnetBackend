using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using store_appV2_BACKEND.Data;
using store_appV2_BACKEND.Models;

namespace store_appV2_BACKEND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispencariesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public DispencariesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Dispencaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dispencary>>> GetDispencaries()
        {
            if (_context.Dispencaries == null)
            {
                return NotFound();
            }
            return await _context.Dispencaries.ToListAsync();
        }

        // GET: api/Dispencaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dispencary>> GetDispencary(int id)
        {
            if (_context.Dispencaries == null)
            {
                return NotFound();
            }
            var dispencary = await _context.Dispencaries.FindAsync(id);

            if (dispencary == null)
            {
                return NotFound();
            }

            return dispencary;
        }

        // PUT: api/Dispencaries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDispencary(int id, Dispencary dispencary)
        {
            if (id != dispencary.Id)
            {
                return BadRequest();
            }

            _context.Entry(dispencary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispencaryExists(id))
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

        // POST: api/Dispencaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Dispencary>> PostDispencary(Dispencary dispencary)
        {
            if (_context.Dispencaries == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Dispencaries'  is null.");
            }
            _context.Dispencaries.Add(dispencary);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (DispencaryExists(dispencary.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDispencary", new { id = dispencary.Id }, dispencary);
        }

        // DELETE: api/Dispencaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDispencary(int id)
        {
            if (_context.Dispencaries == null)
            {
                return NotFound();
            }
            var dispencary = await _context.Dispencaries.FindAsync(id);
            if (dispencary == null)
            {
                return NotFound();
            }

            _context.Dispencaries.Remove(dispencary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DispencaryExists(int id)
        {
            return (_context.Dispencaries?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
