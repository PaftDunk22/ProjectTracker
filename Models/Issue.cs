using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Tracker.Models
{
    public enum Priority
    {
        Low,
        Medium,
        High
    }

    public enum Status
    {
        Open,
        InProgress,
        Closed
    }

    public class Issue
    {
        public int Id { get; set; }

        [StringLength(100)]
        public required string Title { get; set; }

        [StringLength(2000)]
        public string? Description { get; set; }
        public Priority Priority { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int ProjectId { get; set; }

        [ValidateNever]
        public Project Project { get; set; } = null!;
        public ICollection<Comment> Comments { get; private set; } = new List<Comment>();

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

    }
}
