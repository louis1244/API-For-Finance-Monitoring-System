using System.ComponentModel.DataAnnotations;

namespace ApplicationAPIDemo.Models
{
    public class Finances
    {
        [Key]
        public string name { get; set; }
        [Required]
        public string amount { get; set; }
        [Required]
        public string paymentdate { get; set; }
        [Required]
        public string type { get; set; }
    }
}
