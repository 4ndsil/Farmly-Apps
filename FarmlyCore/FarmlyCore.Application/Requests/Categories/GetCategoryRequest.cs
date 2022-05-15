namespace FarmlyCore.Application.Requests.Categories
{
    public class GetCategoryRequest
    {
        public GetCategoryRequest(int categoryId)
        {
            CategoryId = categoryId;
        }

        public int CategoryId { get; set; }
    }
}
