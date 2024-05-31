using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using HandCrafter.DataBase;

namespace HandCrafter.DataBase
{
    public class ProductColorDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public int IdColor { get; set; }
        public ProductDB? Product { get; set; }
        public ColorDB? Color { get; set; }
    }
}

