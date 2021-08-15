using System.ComponentModel.DataAnnotations;

namespace Produktverwaltung.DataAccess.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [MinLength(5)]
        public string Name { get; set; }

        [Range(double.Epsilon, double.MaxValue)]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Required]
        public int? CategoryId { get; set; }
    }
}
