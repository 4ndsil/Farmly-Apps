using FarmlyCore.Application.DTOs.Adverts;

namespace FarmlyCore.Application.Requests.Adverts
{
    public class CreateAdvertItemRequest
    {
        public CreateAdvertItemRequest(CreateAdvertItemDto advertItem, int advertId)
        {
            AdvertItem = advertItem;
            AdvertId = advertId;
        }

        public CreateAdvertItemDto AdvertItem { get; }

        public int AdvertId { get; }
    }
}
