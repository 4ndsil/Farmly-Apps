using FarmlyCore.Application.DTOs.Adverts;
using Microsoft.AspNetCore.JsonPatch;

namespace FarmlyCore.Application.Requests.Adverts
{
    public class UpdateAdvertRequest
    {
        public UpdateAdvertRequest(int advertId, JsonPatchDocument<AdvertDto> advert)
        {
            AdvertId = advertId;
            Advert = advert ?? throw new ArgumentNullException(nameof(advert));
        }

        public int AdvertId { get; set; }
        public JsonPatchDocument<AdvertDto> Advert { get; set; }
    }
}
