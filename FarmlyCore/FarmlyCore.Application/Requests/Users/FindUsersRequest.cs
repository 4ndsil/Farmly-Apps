namespace FarmlyCore.Application.Requests.Users
{
    public class FindUsersRequest
    {
        public FindUsersRequest() { }

        public string? Credentials { get; set; }

        public string? Email { get; set; }

        public int? CustomerId { get; set; }
    }
}