namespace FarmlyCore.Application.Requests.Adverts
{
    public class FindAdvertsRequest
    {
        public FindAdvertsRequest() { }

        public int[] AdvertIds { get; set; } = Array.Empty<int>();

        public int? CustomerId { get; set; }

        public int? CategoryId { get; set; }
    }
}
