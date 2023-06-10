using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eravol.UserWebApi.Data;
using Eravol.WebApi.Data.Models;

namespace Eravol.WebApi.Controllers.Services.Freelancers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Services1Controller : ControllerBase
    {
        private readonly EravolUserWebApiContext _context;

        public Services1Controller(EravolUserWebApiContext context)
        {
            _context = context;
        }

        // GET: api/Services1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Service>>> GetServices()
        {
          if (_context.Services == null)
          {
              return NotFound();
          }
            return await _context.Services.ToListAsync();
        }

        // GET: api/Services1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> GetService(string id)
        {
          if (_context.Services == null)
          {
              return NotFound();
          }
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return service;
        }

        // PUT: api/Services1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutService(string id, Service service)
        {
            if (id != service.ServiceCode)
            {
                return BadRequest();
            }

            _context.Entry(service).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServiceExists(id))
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

        // POST: api/Services1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
          if (_context.Services == null)
          {
              return Problem("Entity set 'EravolUserWebApiContext.Services'  is null.");
          }
            _context.Services.Add(service);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ServiceExists(service.ServiceCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetService", new { id = service.ServiceCode }, service);
        }

        // DELETE: api/Services1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(string id)
        {
            if (_context.Services == null)
            {
                return NotFound();
            }
            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServiceExists(string id)
        {
            return (_context.Services?.Any(e => e.ServiceCode == id)).GetValueOrDefault();
        }
    }
}
