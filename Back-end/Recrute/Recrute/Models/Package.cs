using System.ComponentModel.DataAnnotations;

namespace Recrute.Models
{
    public class Package
    {
        [Key]
        public int Id { get; set; }
        [Required]
     
        public string Lloji { get; set; }
        [Required]
       
        public string Qmimi { get; set; }
        [Required]
      
        public string Nr_Employ { get; set; }
       
    }
}
