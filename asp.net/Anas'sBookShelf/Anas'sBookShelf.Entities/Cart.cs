using AnassBookShelf.Utils.Enums;

namespace Anas_sBookShelf.Entities
{
    public class Cart
    {
        public Cart()
        {
            Books = new List<Book>();
        }
        public int Id { get; set; }
        public string TotalPrice { get; set; }
        public DateTime CheckOutDate { get; set; }
        public CartStatus Status { get; set; } = CartStatus.Open;


        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<Book> Books { get; set; }
    }
}
