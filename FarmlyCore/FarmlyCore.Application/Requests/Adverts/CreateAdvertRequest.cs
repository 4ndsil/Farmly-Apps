using FarmlyCore.Application.DTOs;

namespace FarmlyCore.Application.Queries.Requests.Adverts
{
    public class CreateAdvertRequest
    {
        public AdvertDto Advert { get; }

        public CreateAdvertRequest(AdvertDto advert)
        {
            Advert = advert ?? throw new ArgumentNullException(nameof(advert));
        }
    }
}
