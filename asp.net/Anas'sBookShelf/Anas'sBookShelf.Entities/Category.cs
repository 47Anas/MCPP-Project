namespace Anas_sBookShelf.Entities
{
    public class Category
    {
        public Category()
        {
            Books = new List<Book>();
        }
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Book> Books { get; set; }
    }
}
