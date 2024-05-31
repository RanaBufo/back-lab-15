using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HandCrafter.DataBase
{
    public class CompositionDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<ProductCompositionDB> ProductComposition {  get; set; } = new List<ProductCompositionDB>();
    }
}
