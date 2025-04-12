using System.ComponentModel.DataAnnotations;

namespace Recrute.Models
{
    public class Signup
    {
        [Key]
        public String Username { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Password { get; set; }
       
        
        [Required]
        public int Role { get; set; }

   
    }
}
