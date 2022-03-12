namespace FarmlyCore.Application.Queries.Requests.Customers
{
    public class FindCustomersRequest
    {
        public FindCustomersRequest() { }

        public int[] CustomerIds { get; set; } = Array.Empty<int>();        
    }
}
