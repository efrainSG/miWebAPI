using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPI_demo.Models;

namespace webAPI_demo.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly ListaComprasContext _context;

        public StoreController(ListaComprasContext context)
        {
            _context = context;
        }

        // GET: api/Store
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCompra>>> GetLIstaComprasItems()
        {
            return await _context.LIstaComprasItems.ToListAsync();
        }

        // GET: api/Store/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCompra>> GetItemCompra(int id)
        {
            var itemCompra = await _context.LIstaComprasItems.FindAsync(id);

            if (itemCompra == null)
            {
                return NotFound();
            }

            return itemCompra;
        }

        // PUT: api/Store/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCompra(int id, ItemCompra itemCompra)
        {
            if (id != itemCompra.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCompraExists(id))
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

        // POST: api/Store
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ItemCompra>> PostItemCompra(ItemCompra itemCompra)
        {
            _context.LIstaComprasItems.Add(itemCompra);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemCompra", new { id = itemCompra.Id }, itemCompra);
        }

        // DELETE: api/Store/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemCompra>> DeleteItemCompra(int id)
        {
            var itemCompra = await _context.LIstaComprasItems.FindAsync(id);
            if (itemCompra == null)
            {
                return NotFound();
            }

            _context.LIstaComprasItems.Remove(itemCompra);
            await _context.SaveChangesAsync();

            return itemCompra;
        }

        private bool ItemCompraExists(int id)
        {
            return _context.LIstaComprasItems.Any(e => e.Id == id);
        }
    }
}
