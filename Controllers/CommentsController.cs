using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tracker.Data;
using Tracker.Models;

public class CommentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public CommentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Create(int issueId)
    {
        var issue = await _context.Issues.FindAsync(issueId);

        if (issue == null) return NotFound();

        ViewBag.IssueTitle = issue.Title;

        var comment = new Comment
        {
            Content = string.Empty,
            IssueId = issueId
        };

        return View(comment);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Comment comment)
    {
        if (!ModelState.IsValid)
        {
            var issue = await _context.Issues.FindAsync(comment.IssueId);
            if (issue == null) return NotFound();

            ViewBag.IssueTitle = issue.Title;
            return View(comment);
        }

        var existingIssue = await _context.Issues.FindAsync(comment.IssueId);

        if (existingIssue == null) return NotFound();

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Issues", new { id = comment.IssueId });
    }


    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var comment = await _context.Comments.FindAsync(id);

        if (comment == null) return NotFound();

        int issueId = comment.IssueId;

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", "Issues", new { id = issueId });
    }

}