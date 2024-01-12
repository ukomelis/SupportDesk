using System.ComponentModel.DataAnnotations;

namespace SupportDeskAPI.Models
{
    public class SupportRequest
    {
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public required string Title { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required DateTime Deadline { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool Resolved { get; set; }
    }
}