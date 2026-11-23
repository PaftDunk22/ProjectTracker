using System.ComponentModel.DataAnnotations;

namespace Tracker.Models
{
    public class Project
    {
        public int Id { get; set; }

        [StringLength(100)]
        public required string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Issue> Issues { get; private set; } = new List<Issue>();

        public void AddIssue(Issue issue )
        {
            Issues.Add(issue);
        }

    }
}
