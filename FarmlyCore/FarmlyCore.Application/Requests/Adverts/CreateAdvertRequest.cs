using FarmlyCore.Application.DTOs.Adverts;

namespace FarmlyCore.Application.Requests.Adverts
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
