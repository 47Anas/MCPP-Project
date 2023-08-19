namespace Anas_sBookShelf.Entities
{
    public class Order
    {
        public Order()
        {
            Books = new List<Book>();
        }
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public string Note { get; set; } 
        public DateTime OrderDate { get; set; }


        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<Book> Books { get; set; }
    }
}
