namespace FarmlyCore.Infrastructure.Entities
{
    public class CustomerAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
