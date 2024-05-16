using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IceCreamApi;
using IceCreamApi.Domain;

namespace IceCreamApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IceCreamController : ControllerBase
    {
        private readonly IceCreamShopContext _context;

        public IceCreamController(IceCreamShopContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IceCream>>> GetIceCreams()
        {
            return await _context.IceCreams.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IceCream>> GetIceCream(Guid? id)
        {
            var iceCream = await _context.IceCreams.FindAsync(id);

            if (iceCream == null)
            {
                return NotFound();
            }

            return iceCream;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIceCream(Guid? id, IceCream iceCream)
        {
            if (id != iceCream.Id)
            {
                return BadRequest();
            }

            _context.Entry(iceCream).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IceCreamExists(id))
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
        
        [HttpPost]
        public async Task<ActionResult<IceCream>> PostIceCream(IceCream iceCream)
        {
            _context.IceCreams.Add(iceCream);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostIceCream), new { id = iceCream.Id }, iceCream);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIceCream(Guid? id)
        {
            var iceCream = await _context.IceCreams.FindAsync(id);
            if (iceCream == null)
            {
                return NotFound();
            }

            _context.IceCreams.Remove(iceCream);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IceCreamExists(Guid? id)
        {
            return _context.IceCreams.Any(e => e.Id == id);
        }
    }
}
