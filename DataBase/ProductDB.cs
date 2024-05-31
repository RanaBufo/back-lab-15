using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HandCrafter.DataBase
{
    public class ProductDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
        public string? Description { get; set; }
        public string? ImgName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public int Quantity { get; set; }
        public ICollection<ProductCompositionDB> ProductComposition {  get; set; } = new List<ProductCompositionDB>();
        public ICollection<ProductColorDB> ProductColor {  get; set; } =   new List<ProductColorDB>();
        public ICollection<ProductCategoryDB> ProductCategory { get; set; } = new List<ProductCategoryDB>();
        public ICollection<BasketDB> Basket { get; set; } = new List<BasketDB>();
    
    }
}
