using System.ComponentModel.DataAnnotations;
namespace Recrute.Models
{
    public class Password
    {
        [Key]
        public string OldPassword { get; set; }
        [Required] 
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
