using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HandCrafter.DataBase;

namespace HandCrafter.DataBase
{
    public class ProductCategoryDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public int IdCategory { get; set; }
        public ProductDB? Product { get; set; } 
        public CategoryDB? Category { get; set; } 
    }
}

