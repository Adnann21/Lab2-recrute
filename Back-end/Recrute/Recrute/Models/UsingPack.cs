using System.ComponentModel.DataAnnotations;

namespace Recrute.Models
{
    public class UsingPack
    {
        [Key]
        public int Id { get; set; }
 
        [Required]
        public string RecrComp { get; set; }
        [Required]
        public DateTime Exp_Day { get; set; }
    }
}
