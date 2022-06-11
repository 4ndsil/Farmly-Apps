using System.ComponentModel.DataAnnotations;

namespace FarmlyCore.Application.DTOs.Customer
{
    public class CreateCustomerDto
    {
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string Email { get; set; }
        public CustomerTypeDto CustomerType { get; set; }
        public string BankGiro { get; set; }
        public string OrgNumber { get; set; }
        public ICollection<CustomerAddressDto> CustomerAddresses { get; set; } = Array.Empty<CustomerAddressDto>();
    }
}
