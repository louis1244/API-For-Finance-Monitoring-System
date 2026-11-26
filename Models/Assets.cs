using System.ComponentModel.DataAnnotations;

namespace ApplicationAPIDemo.Models
{
    public class Assets
    {
        [Key]
        public string name { get; set; }
        [Required]
        public string amount { get; set; }
    }
}
