using HandCrafter.DataBase;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HandCrafter.Model
{
    public class BasketRequest
    {
        public int IdUser { get; set; }
        public int IdProduct { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
