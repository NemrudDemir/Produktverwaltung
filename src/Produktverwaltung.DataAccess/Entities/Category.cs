using System.ComponentModel.DataAnnotations;

namespace Produktverwaltung.DataAccess.Entities
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
