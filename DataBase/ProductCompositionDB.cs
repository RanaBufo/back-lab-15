using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HandCrafter.DataBase;

namespace HandCrafter.DataBase
{
    public class ProductCompositionDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public int IdComposition { get; set; }
        public ProductDB? Product { get; set; }
        public CompositionDB? Composition { get; set; }
    }
}

