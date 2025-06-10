using System.ComponentModel.DataAnnotations;

namespace Recrute.Models
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        public string Username { get; set; } // Link to Users.username

        [Required]
        public DateTime ExpiryDate { get; set; }
    }
}
