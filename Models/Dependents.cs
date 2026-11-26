using System.ComponentModel.DataAnnotations;

namespace ApplicationAPIDemo.Models
{
    public class Dependents
    {
        [Key]
        public string name{ get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string dob { get; set; }
        [Required]
        public string id { get; set; }
        [Required]
        public string keylink { get; set; }
    }
}
