using System.ComponentModel.DataAnnotations;

namespace HandCrafter.Model
{
    public class AddressRequestModel
    {
        public string Country { get; set; }
        public string? Region { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Entrance { get; set; }
        public string Room { get; set; }
        public string Intercom { get; set; }
    }
}
