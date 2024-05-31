using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HandCrafter.DataBase
{
    public class CategoryDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<ProductCategoryDB> ProductCategory { get; set; } = new List<ProductCategoryDB>();
    }
}
