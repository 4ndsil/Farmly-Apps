namespace FarmlyCore.Application.Requests.Customers
{
    public class FindUsersRequest
    {
        public FindUsersRequest() { }

        public string? Credentials { get; set; }

        public string? Email { get; set; }

        public string? CustomerId { get; set; }
    }
}