namespace FarmlyCore.Application.DTOs.Customer
{
    public class CustomerAddressDto
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public int CustomerId { get; set; }
    }
}
