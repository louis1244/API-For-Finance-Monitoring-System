using System.ComponentModel.DataAnnotations;

namespace ApplicationAPIDemo.Models
{
    public class Members
    {
        [Key]
        public string Name { get; set; }
        [Required]
        public string Surname{ get; set; }
        [Required]
        public string id { get; set; }
        [Required]
        public string phonenumber { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string address { get; set; }
        [Required]
        public string dob { get; set; }
        [Required]
        public string jod { get; set; }
        [Required]
        public string keylink { get; set; }

    }
}
