using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace Recrute.Models
{
    public class Applicants
    {
        [Key]
        public String Username { get; set; }
        [Required]
        public string File_Cv { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public int Review { get; set; }
        [Required]
        public string RecrComp { get; set; }
      
    }
}
