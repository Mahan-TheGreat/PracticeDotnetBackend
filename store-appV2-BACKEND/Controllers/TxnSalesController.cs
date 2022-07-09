using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using store_appV2_BACKEND.Data;
using store_appV2_BACKEND.Models;

namespace store_appV2_BACKEND.Controllers
{

    [EnableCors("_myAllowSpecificOrigins")]
    [Route("api/[controller]")]
    [ApiController]
    public class TxnSalesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TxnSalesController(ApplicationDBContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult> GetSale()
        {

            if (_context.TxnSales == null)
            {
                return NotFound();
            }

            var result = await (from txn in _context.TxnSales
                                join inv in _context.Inventories
                                on txn.ItemId equals inv.Id
                                select new
                                {
                                    id = txn.Id,
                                    itemName = inv.ItemName,
                                    quantity = txn.Quantity,
                                    totalPrice = txn.TotalPrice
                                }).ToListAsync();



            return Ok(result);
        }

        [HttpGet("/api/TxnSales/GenReport")]

        public async Task<ActionResult> GenReport()
        {
            if (_context.TxnSales == null)
            {
                return NotFound();
            }
            //var sale = await _context.TxnSales.FindAsync(id);

            //var result = await (from txn in _context.TxnSales
            //                    join inv in _context.Inventories
            //                    on txn.ItemId equals inv.Id
            //                    select new
            //                    {
            //                        id = txn.Id,
            //                        itemName = inv.ItemName,
            //                        quantity = txn.Quantity,
            //                        totalPrice = txn.TotalPrice
            //                    }).ToListAsync();

            var result = await _context.TxnSales
                        .Join(_context.Inventories,
                            sales => sales.ItemId,
                            inv => inv.Id,
                            (sales, inv) => new
                            {
                                id = sales.Id,
                                itemName = inv.ItemName,
                                quantity = sales.Quantity,
                                totalPrice = sales.TotalPrice
                            }).GroupBy(a => a.itemName).Select(g => new
                            {
                                ItemName = g.Key,
                                quantity = g.Sum(a=>a.quantity),
                                totalPrice = g.Sum(a => a.totalPrice)
                            }).ToListAsync();

            //var sale2 = result.GroupBy(x => x.itemName).ToList();

            return Ok(result);
        }


        [HttpGet("{id}")]

        public async Task<ActionResult<TxnSale>> GetSale(int id)
        {
            if (_context.TxnSales == null)
            {
                return NotFound();
            }
            //var sale = await _context.TxnSales.FindAsync(id);

            var sale = await _context.TxnSales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(int id, TxnSale sale)
        {
            if (id != sale.Id)
            {
                return BadRequest();
            }

            _context.Entry(sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalesExists(id))
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

     

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TxnSale>> PostSale(TxnSale sale)
        {
            if (_context.TxnSales == null)
            {
                return Problem("Entity set 'ApplicationDBContext.TxnSales'  is null.");
            }
            _context.TxnSales.Add(sale);
           var x = _context.Inventories.First(e=>e.Id == sale.ItemId).Quantity-=sale.Quantity;
            Console.WriteLine(x);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SalesExists(sale.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { status = 200, id = sale.Id });
        }

        // DELETE: api/Inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            if (_context.TxnSales == null)
            {
                return NotFound();
            }
            var sale = await _context.TxnSales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.TxnSales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SalesExists(int id)
        {
            return (_context.TxnSales?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
