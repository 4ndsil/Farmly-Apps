using System.ComponentModel.DataAnnotations.Schema;

namespace FarmlyCore.Infrastructure.Entities
{
    public class Customer
    {
        public Customer() { }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public CustomerType CustomerType { get; set; }
        public string BankGiro { get; set; }
        public string OrgNumber { get; set; }
        public string Phone { get; set; }
        public ICollection<CustomerAddress> CustomerAddresses { get; set; }
    }

    public enum CustomerType
    {
        Farmer,
        Restaurant
    }
}
