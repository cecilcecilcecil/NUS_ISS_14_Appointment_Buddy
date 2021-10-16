using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HSPSpecialist.DBContext;
using HSPSpecialist.Models;

namespace HSPSpecialist.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistsController : ControllerBase
    {
        private readonly SpecialistContext _context;

        public SpecialistsController(SpecialistContext context)
        {
            _context = context;
        }

        // GET: api/Specialists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Specialist>>> GetSpecialist()
        {
           // return await _context.Specialist.ToListAsync();
            var result = await (from Spec in _context.Specialist
                                join ser in _context.Service on Spec.Services equals ser.Id
                                select new Specialist
                                {
                                    Id = Spec.Id,
                                    Name = Spec.Name,
                                    NRIC = Spec.NRIC,
                                    Services = Spec.Services,
                                    Contact = Spec.Contact,
                                    Available = Spec.Available,
                                    Address = Spec.Address,
                                    Email = Spec.Email,
                                    CreatedBy = Spec.CreatedBy,
                                    CreatedDate = Spec.CreatedDate,
                                    LastUpdatedBy = Spec.LastUpdatedBy,
                                    LastUpdatedDate = Spec.LastUpdatedDate,
                                    IsDeleted = Spec.IsDeleted,
                                    ServiceDescription = ser.Description
                                }
                                ).ToListAsync();
            return result;
        }

        // GET: api/Specialists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Specialist>>> GetSpecialist(int id)
        {
            // var specialist = await _context.Specialist.FindAsync(id);

            var specialist = await (from Spec in _context.Specialist
                                join ser in _context.Service on Spec.Services equals ser.Id
                                where Spec.Id == id
                                select new Specialist
                                {
                                    Id = Spec.Id,
                                    Name = Spec.Name,
                                    NRIC = Spec.NRIC,
                                    Services = Spec.Services,
                                    Contact = Spec.Contact,
                                    Available = Spec.Available,
                                    Address = Spec.Address,
                                    Email = Spec.Email,
                                    CreatedBy = Spec.CreatedBy,
                                    CreatedDate = Spec.CreatedDate,
                                    LastUpdatedBy = Spec.LastUpdatedBy,
                                    LastUpdatedDate = Spec.LastUpdatedDate,
                                    IsDeleted = Spec.IsDeleted,
                                    ServiceDescription = ser.Description
                                }
                                   ).ToListAsync();


            if (specialist == null)
            {
                return NotFound();
            }

            return specialist;
        }

        // PUT: api/Specialists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSpecialist(int id, Specialist specialist)
        {
            if (id != specialist.Id)
            {
                return BadRequest();
            }

            _context.Entry(specialist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SpecialistExists(id))
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

        // POST: api/Specialists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Specialist>> PostSpecialist(Specialist specialist)
        {
            _context.Specialist.Add(specialist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSpecialist", new { id = specialist.Id }, specialist);
        }

        // DELETE: api/Specialists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSpecialist(int id)
        {
            var specialist = await _context.Specialist.FindAsync(id);
            if (specialist == null)
            {
                return NotFound();
            }

            _context.Specialist.Remove(specialist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SpecialistExists(int id)
        {
            return _context.Specialist.Any(e => e.Id == id);
        }
    }
}
