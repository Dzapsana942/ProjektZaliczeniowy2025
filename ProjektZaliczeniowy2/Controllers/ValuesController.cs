using Microsoft.AspNetCore.Mvc;
using ProjektZaliczeniowy2.Data;
using ProjektZaliczeniowy2.Models;

namespace ProjektZaliczeniowy2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAll), new { id = user.Id }, user);
        }
    }
}
