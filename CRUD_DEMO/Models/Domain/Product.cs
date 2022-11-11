using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD_DEMO.Models.Domain
{
    public class Product
    {
        public Guid Id { get; set; }

        [Unique(ErrorMessage = "Name already exist!")]
        [Required(ErrorMessage = "Please enter name")]
        [StringLength(100, ErrorMessage = "Name length can't be more than 100.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter price")]
        [Range(0, 999.99)] 
        public decimal Price { get; set; }
    }
}
