namespace Anas_sBookShelf.Entities
{
    public class Book
    {
        public Book()
        {
            Categories = new List<Category>();

            Carts = new List<Cart>();

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public List<Cart> Carts { get; set; }
        public List<Category> Categories { get; set; }
    }
}
