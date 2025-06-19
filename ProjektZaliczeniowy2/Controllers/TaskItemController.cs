using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjektZaliczeniowy2.Data;
using ProjektZaliczeniowy2.Models;
using Microsoft.AspNetCore.Authorization;


namespace ProjektZaliczeniowy2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskItemController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskItemController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TaskItem>> GetAll()
        {
            return Ok(_context.Tasks
                .Include(t => t.User)
                .Include(t => t.Project)
                .ToList());
        }

        [Authorize]
        [HttpPost]
        public ActionResult<TaskItem> Create(TaskItem taskItem)
        {
            _context.Tasks.Add(taskItem);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetAll), new { id = taskItem.Id }, taskItem);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskItem> GetById(int id)
        {
            var taskItem = _context.Tasks
                .Include(t => t.User)
                .Include(t => t.Project)
                .FirstOrDefault(t => t.Id == id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return Ok(taskItem);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var taskItem = _context.Tasks.Find(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskItem);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
