using System.ComponentModel.DataAnnotations;

namespace Recrute.Models
{
    public class Jobs
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompName { get; set; }
        [Required]
        public string Pozition { get; set; }
        [Required]
        public string CompLocation { get; set; }
        [Required]
        public string RecrComp { get; set; }
        [Required]
        public DateTime DataExp { get; set; }
    }
}
