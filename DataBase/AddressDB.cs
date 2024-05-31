using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HandCrafter.DataBase
{
    public class AddressDB
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int IdUser {  get; set; }
        [Required]
        public string Country { get; set; }
        public string? Region { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string House { get; set; }
        [Required]
        public string Entrance { get; set; }
        [Required]
        public string Room { get; set; }
        [Required]
        public string Intercom { get; set; }
        public UserDB? User { get; set; } = null;
    }
}
