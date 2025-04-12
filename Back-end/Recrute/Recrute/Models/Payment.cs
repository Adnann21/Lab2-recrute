using System.ComponentModel.DataAnnotations;
namespace Recrute.Models
{
    public class Payment
    {
       
        [Required]
        public string Email { get; set; }
        [Required] 
        public string CardHolder { get; set; }
        [Required]
        public long CardNumber { get; set; }
        [Required]  
        public string MMYY { get; set; }

        [Required]
       
        public int CVV { get; set; }
        [Required]
        public double Price { get; set; }

    }
}
