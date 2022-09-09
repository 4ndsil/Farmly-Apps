using FarmlyCore.Application.DTOs.Adverts;
using Microsoft.AspNetCore.JsonPatch;

namespace FarmlyCore.Application.Requests.AdvertItems
{
    public class UpdateAdvertItemRequest
    {
        public UpdateAdvertItemRequest(int advertItemId, JsonPatchDocument<AdvertItemDto> advertItem)
        {
            AdvertItemId = advertItemId;

            AdvertItem = advertItem ?? throw new ArgumentNullException(nameof(advertItem));
        }

        public int AdvertItemId { get; set; }

        public JsonPatchDocument<AdvertItemDto> AdvertItem { get; set; }
    }
}
