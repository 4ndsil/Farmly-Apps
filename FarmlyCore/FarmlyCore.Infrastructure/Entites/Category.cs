namespace FarmlyCore.Infrastructure.Entities
{
    public class Category
    {
        public Category() { }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public CategoryType CategoryType { get; set; }
    }

    public enum CategoryType
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
