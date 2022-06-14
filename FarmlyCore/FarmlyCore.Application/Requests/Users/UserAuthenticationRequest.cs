namespace FarmlyCore.Application.Requests.Users
{
    public class UserAuthenticationRequest
    {
        public UserAuthenticationRequest(string email, string credentials)
        {
            Email = email;

            InputCredentials = credentials;
        }

        public string Email { get; set; }

        public string InputCredentials { get; set; }
    }
}