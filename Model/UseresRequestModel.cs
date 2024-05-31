using HandCrafter.DataBase;
using System.ComponentModel.DataAnnotations;

namespace HandCrafter.Model
{
    public class UseresRequestModel
    {
        public string FirstName { get; set; }  
        public string LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? Description { get; set; }
        public DateOnly Birhday { get; set; }
        public ContactRequestModel Contact { get; set; }
        public List<AddressRequestModel>? Address {  get; set; }


    }
}
