using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webAPI_demo.Models;

namespace webAPI_demo.Controllers {

    [ApiController, Route("api/Compras")]
    public class ComprasController : ControllerBase {
        private readonly ListaComprasContext _context;

        public ComprasController(ListaComprasContext context) {
            _context = context;
        }

        [HttpGet("Init/{count}/{maximum}")]
        public async Task<ActionResult> initContext(int count, int maximum) {
            var _aleatorio = new Random(100);
            int i = 1;

                _context.LIstaComprasItems.Add(new ItemCompra {
                    Cantidad = _aleatorio.Next(maximum > 20 ? maximum : 20),
                    Comprado = (_aleatorio.Next(maximum > 20 ? maximum : 20) % 2) == 0,
                    Id = i,
                    Nombre = string.Format("Demo {0} {1}", i, DateTime.Today.Day)
                });

            var resultado = await _context.SaveChangesAsync();
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCompra>> getItem(int id) {
            var _item = await _context.LIstaComprasItems.FindAsync(id);
            if (_item == null)
                return NotFound();
            else
                return _item;
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IEnumerable<ItemCompra>>> getItems() {
            return await _context.LIstaComprasItems.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<ItemCompra>> PostItem(ItemCompra item) {
            _context.LIstaComprasItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(getItem), new { id = item.Id }, item );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutItem (int id, ItemCompra item) {
            if (id != item.Id)
                return BadRequest();

            _context.Entry(item).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            } catch (DbUpdateConcurrencyException DBUCEx) {
                if (!itemCompraExiste(id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemCompra>> deleteItem(int id) {
            var item = await _context.LIstaComprasItems.FindAsync(id);
            if (item == null)
                return NotFound();

            _context.LIstaComprasItems.Remove(item);
            await _context.SaveChangesAsync();

            return item;
        } 

        private bool itemCompraExiste(int id) => _context.LIstaComprasItems.Any(i => i.Id.Equals(id));
    }
}
