using System.ComponentModel.DataAnnotations;
namespace Recrute.Models
{
    public class Payment
    {

        [Required] 
        public string cardholderName { get; set; }
        [Required]
        public long cardNumber { get; set; }
        [Required]  
        public string expiryDate { get; set; }

        [Required]
       
        public int cvv { get; set; }
        [Required]
        public long totalPrice { get; set; }
        [Required]
        public int userCount { get; set; }

    }
}
