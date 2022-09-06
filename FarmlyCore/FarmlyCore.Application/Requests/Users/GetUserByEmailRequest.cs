namespace FarmlyCore.Application.Requests.Users
{
    public class GetUserByEmailRequest
    {
        public GetUserByEmailRequest(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}