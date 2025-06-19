using Microsoft.AspNetCore.Mvc;
using ProjektZaliczeniowy2.Data;
using ProjektZaliczeniowy2.Models;

namespace ProjektZaliczeniowy2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Project
        [HttpGet]
        public IActionResult GetAllProjects()
        {
            var projects = _context.Projects.ToList();
            return Ok(projects);
        }

        // GET: api/Project/{id}
        [HttpGet("{id}")]
        public IActionResult GetProjectById(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        // POST: api/Project
        [HttpPost]
        public IActionResult AddProject([FromBody] Project project)
        {
            if (project == null)
            {
                return BadRequest();
            }

            _context.Projects.Add(project);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, project);
        }

        // PUT: api/Project/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateProject(int id, [FromBody] Project updatedProject)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            project.Title = updatedProject.Title;
            // Jeśli masz inne pola, zaktualizuj je tutaj

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/Project/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteProject(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
