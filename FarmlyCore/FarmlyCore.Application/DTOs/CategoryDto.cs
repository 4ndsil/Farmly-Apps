namespace FarmlyCore.Application.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public CategoryTypeDto CategoryType { get; set; }
    }

    public enum CategoryTypeDto
    {
        Kött,
        Fisk,
        Grönsaker,
        Frukt,
        Bär,
        Dryck,
        Svamp,
        Torrvaror,
        Konserver,
        Vilt,
        Frys,
        Mejeri,
        Övrigt
    }
}
