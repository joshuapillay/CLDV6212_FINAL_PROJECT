using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CLDV6212_FINAL_PROJECT.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] 
        public decimal Price { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
