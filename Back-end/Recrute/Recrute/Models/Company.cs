using System.ComponentModel.DataAnnotations;

namespace Recrute.Models
{
    public class Company
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public String RecruteComp { get; set; }
     
        [Required]
        public string Location { get; set; }

    }
}
