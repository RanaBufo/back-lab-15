using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HandCrafter.DataBase
{
    public class BasketDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required]
        public int IdUser { get; set; }
        [Required]
        public int IdProduct { get; set; }
        [Required]
        public double Discount { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public UserDB? User { get; set; } 
        public ProductDB? Product { get; set; } 
    }
}
