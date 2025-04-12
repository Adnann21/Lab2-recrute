using System.ComponentModel.DataAnnotations;

namespace Recrute.Models
{
    public class RecruteComp
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public String RecrComp { get; set; }
     
        [Required]
        public int Nr_Employ { get; set; }

    }
}
