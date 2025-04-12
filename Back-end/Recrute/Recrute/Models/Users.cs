using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Recrute.Models
{
    public class Users
    {
        [Key]
        public String username { get; set; }
        [Required]
        public String Email { get; set; }
       
        [Required]
        [DataType(DataType.Password)]
        public String Password { get; set; }
        [Required]
        public int Role { get; set; }
        
      
    }
}
