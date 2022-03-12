using FarmlyCore.Application.DTOs;
using Microsoft.AspNetCore.JsonPatch;

namespace FarmlyCore.Application.Queries.Requests.Customer
{
    public class UpdateAdvertRequest
    {
        public UpdateAdvertRequest(int customerId, JsonPatchDocument<AdvertDto> advert)
        {
            AdvertId = customerId;
            Advert = advert ?? throw new ArgumentNullException(nameof(advert));
        }

        public int AdvertId { get; set; }
        public JsonPatchDocument<AdvertDto> Advert { get; set; }
    }
}
