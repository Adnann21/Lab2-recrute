using System.ComponentModel.DataAnnotations;

namespace Recrute.Models
{
    public class Workers
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompName { get; set; }
        [Required]
        public string Pozition { get; set; }
        [Required]
        public string Ap_Username { get; set; }
        [Required]
        public decimal Payment { get; set; }
        [Required]
        public string Emp_Username { get; set; }
    }
}
