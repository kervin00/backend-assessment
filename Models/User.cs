using System.ComponentModel.DataAnnotations;

namespace Assessment.Models
{
    #pragma warning disable CS8618
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
    #pragma warning restore CS8618
}
