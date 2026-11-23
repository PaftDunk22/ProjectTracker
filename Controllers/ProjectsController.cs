using Microsoft.AspNetCore.Mvc;
using Tracker.Data;
using Microsoft.EntityFrameworkCore;
using Tracker.Models;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Tracker.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects
                .Include(p => p.Issues)
                .ToListAsync();

            return View(projects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Project p)
        {

            if (!ModelState.IsValid)
            {
                return View(p);
            }

            _context.Projects.Add(p);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var project = await _context.Projects
                .Include(p => p.Issues)
                    .ThenInclude(i => i.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null) return NotFound();

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Project p)
        {
            if (!ModelState.IsValid) return View(p);

            var project = await _context.Projects.FindAsync(p.Id);

            if(project==null) return NotFound();

            project.Name = p.Name;
            project.Description = p.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var project = await _context.Projects.FindAsync(id);

            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var project = await _context.Projects.FindAsync(id);

            if (project == null) return NotFound();

            return View(project);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project == null) return NotFound();

            _context.Projects.Remove(project);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
