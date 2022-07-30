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
    public class UserCredsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UserCredsController(ApplicationDBContext context)
        {
            _context = context;
        }



        [HttpPost]
        public async Task<ActionResult> PostUserCreds(UserCred user1)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Userscreds'  is null.");
            }
            _context.UserCreds.Add(user1);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsercredExists(user1.Id))
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

        private bool UsercredExists(int id)
        {
            return (_context.UserCreds?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
