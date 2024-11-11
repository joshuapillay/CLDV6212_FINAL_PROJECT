using System;
using System.ComponentModel.DataAnnotations;

namespace CLDV6212_FINAL_PROJECT.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Please select a product.")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }  

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Please select a customer.")]
        public int CustomerProfileId { get; set; }
        public CustomerProfile? CustomerProfile { get; set; }  
    }
}
