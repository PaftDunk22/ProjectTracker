using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.Models;

namespace Tracker.Controllers
{
    public class IssuesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IssuesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Create(int projectId)
        {

            var project = await _context.Projects.FindAsync(projectId);

            if(project == null) return NotFound();

            Issue issue = new() { Title = String.Empty, ProjectId = projectId };

            ViewBag.ProjectName = project.Name;

            return View(issue);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Issue issue)
        {
            if (!ModelState.IsValid)
            {
                var project = await _context.Projects.FindAsync(issue.ProjectId);
                if (project == null) return NotFound();
                ViewBag.ProjectName = project.Name;
                return View(issue);
            }

            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Projects", new { id = issue.ProjectId });
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null) return NotFound();

            Console.WriteLine("before include and firstordefault...");
            var Issue = await _context.Issues
                .Include(i => i.Comments)
                .Include(i => i.Project)
                .FirstOrDefaultAsync(i => i.Id == id);


            Console.WriteLine("after include and firstordefault...");

            if (Issue == null) return NotFound();

            ViewBag.ProjectName = Issue.Project.Name;

            return View(Issue);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var issue = await _context.Issues.FindAsync(id);

            if (issue == null) return NotFound();

            return View(issue);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Issue issue)
        {
            if (!ModelState.IsValid) return View(issue);

            var Issue = await _context.Issues.FindAsync(issue.Id);

            if (Issue == null) return NotFound();

            Issue.Title = issue.Title;
            Issue.Description = issue.Description;
            Issue.Status = issue.Status;
            Issue.Priority = issue.Priority;

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Issues", new { id = issue.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var issue = await _context.Issues.FirstOrDefaultAsync(i => i.Id == id);

            if (issue == null) return NotFound();

            return View(issue);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmation(int id)
        {
            var issue = await _context.Issues.FindAsync(id);

            if (issue == null) return NotFound();

            int projectId = issue.ProjectId;

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Projects", new { id = projectId });
        }

    }
}
