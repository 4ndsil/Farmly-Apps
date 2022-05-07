using FarmlyCore.Application.DTOs.Adverts;

namespace FarmlyCore.Application.Requests.Adverts
{
    public class CreateAdvertRequest
    {
        public CreateAdvertDto Advert { get; }

        public CreateAdvertRequest(CreateAdvertDto advert)
        {
            Advert = advert ?? throw new ArgumentNullException(nameof(advert));
        }
    }
}
