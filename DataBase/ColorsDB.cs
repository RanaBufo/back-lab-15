using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HandCrafter.DataBase
{
    public class ColorDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<ProductColorDB> ProductColor {  get; set; } = new List<ProductColorDB>();
    }
}
