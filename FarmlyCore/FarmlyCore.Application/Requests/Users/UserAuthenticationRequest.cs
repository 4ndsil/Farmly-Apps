namespace FarmlyCore.Application.Requests.Users
{
    public class UserAuthenticationRequest
    {
        public UserAuthenticationRequest(string email, string credentials)
        {
            Email = email;

            Credentials = credentials;
        }

        public string Email { get; set; }

        public string Credentials { get; set; }
    }
}