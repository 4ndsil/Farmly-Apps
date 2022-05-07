using System.ComponentModel.DataAnnotations.Schema;

namespace FarmlyCore.Infrastructure.Entities
{
    public class CustomerAddress
    {
        public CustomerAddress() { }

        public CustomerAddress(Customer customer) 
        { 
            Customer = customer;
        }

        public int Id { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public int Zip { get; set; }

        public Customer Customer { get; set; }

        public int FkCustomerId { get; set; }                
    }
}
