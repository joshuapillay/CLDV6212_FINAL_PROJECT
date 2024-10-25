using System.ComponentModel.DataAnnotations;

namespace CLDV6212_FINAL_PROJECT.Models
{
    

    public class CustomerProfile
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }
    }
}
