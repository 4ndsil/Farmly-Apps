namespace FarmlyCore.Application.Requests.Users
{
    public class UserAuthenticationRequest
    {
        public UserAuthenticationRequest(string email, string password)
        {
            Email = email;

            Password = password;
        }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}