using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
namespace Recrute.Models
{
    public class Candidate
    {
        [Key]
        public String Username { get; set; }
        
        public string File_Cv { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public int Review { get; set; }
        
        public string RecrComp { get; set; }
      
    }
}
