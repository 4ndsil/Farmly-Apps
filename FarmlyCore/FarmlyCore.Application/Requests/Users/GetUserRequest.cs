namespace FarmlyCore.Application.Requests.Users
{
    public class GetUserRequest
    {
        public GetUserRequest(string password)
        {
            Password = password;
        }

        public string Password { get; set; }
    }
}