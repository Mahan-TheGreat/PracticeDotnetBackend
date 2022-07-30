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
    public class UsersCredsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UsersCredsController(ApplicationDBContext context)
        {
            _context = context;
        }



        [HttpPost]
        public async Task<ActionResult> PostInventory(UsersCred user1)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Users'  is null.");
            }
            _context.UsersCreds.Add(user1);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserscredExists(user1.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return Ok(new { status = 200, id = user1.Id });
        }

        private bool UserscredExists(int id)
        {
            return (_context.UsersCreds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
