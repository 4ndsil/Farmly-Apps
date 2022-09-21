using FarmlyCore.Application.DTOs.Customer;

namespace FarmlyCore.Application.Requests.Customers
{
    public class FindCustomersRequest
    {
        public FindCustomersRequest() { }

        public CustomerTypeDto? CustomerType { get; set; }

        public int [] CustomerIds { get; set; } = Array.Empty<int>();

        public string? CompanyName { get; set; }
    }
}