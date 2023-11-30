using System.ComponentModel.DataAnnotations;

namespace Assessment.Models
{
    public class TaskManagement
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        public DateTime? CreationDate { get; set; } // Nullable DateTime
        public bool Status { get; set; }
    }
}
