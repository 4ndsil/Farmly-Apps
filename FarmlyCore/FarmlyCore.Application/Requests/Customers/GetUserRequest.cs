namespace FarmlyCore.Application.Requests.Customers
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