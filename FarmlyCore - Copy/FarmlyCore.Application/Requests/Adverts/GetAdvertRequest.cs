namespace FarmlyCore.Application.Requests.Adverts
{
    public class GetAdvertRequest
    {
        public GetAdvertRequest(int advertId)
        {
            AdvertId = advertId;
        }

        public int AdvertId { get; set; }
    }
}
