namespace FarmlyCore.Application.Requests.Users
{
    public class GetUserRequest
    {
        public GetUserRequest(int userId)
        {
            UserId = userId;
        }

        public int UserId { get; set; }
    }
}