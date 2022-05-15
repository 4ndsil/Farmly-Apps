namespace FarmlyCore.Application.Requests.Adverts
{
    public class FindAdvertsRequest
    {
        public FindAdvertsRequest() { }        

        public int? CustomerId { get; set; }

        public int? CategoryId { get; set; }

        public string? ProductName { get; set; }
    }
}
