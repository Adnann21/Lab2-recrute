using System.ComponentModel.DataAnnotations;

namespace Recrute.Models
{
    public class JobStatus
    {
        [Key]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string CompName { get; set; }
        [Required]
        [StringLength(100)]
        public string Pozition { get; set; }
        [Required]
        [StringLength(100)]
        public string StatusJob { get; set; }

    }
}
