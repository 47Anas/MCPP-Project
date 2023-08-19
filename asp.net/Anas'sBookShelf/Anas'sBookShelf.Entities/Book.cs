namespace Anas_sBookShelf.Entities
{
    public class Book
    {
        public Book()
        {
            Categories = new List<Category>();

            Orders = new List<Order>();

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

        public List<Order> Orders { get; set; }
        public List<Category> Categories { get; set; }
    }
}
