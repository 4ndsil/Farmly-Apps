namespace FarmlyCore.Application.Requests.Users
{
    public class FindUsersRequest
    {
        public FindUsersRequest() { }

        public string? Email { get; set; }

        public int? CustomerId { get; set; }
    }
}