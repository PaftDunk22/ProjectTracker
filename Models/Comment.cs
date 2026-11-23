using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Tracker.Models
{
    public class Comment
    {
        public int Id {  get; set; }

        [StringLength(1000)]
        public required string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int IssueId { get; set; }

        [ValidateNever]
        public Issue Issue { get; set; } = null!;

    }
}
