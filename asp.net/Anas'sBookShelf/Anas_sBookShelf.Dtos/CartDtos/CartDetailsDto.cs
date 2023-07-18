using Anas_sBookShelf.Entities;
using AnassBookShelf.Utils.Enums;

namespace Anas_sBookShelf.Dtos.CartDtos
{
    public class CartDetailsDto
    {
        public int Id { get; set; }
        public string CustomerFullName { get; set; }
        public string TotalPrice { get; set; }
        public DateTime CheckOutDate { get; set; }
        public CartStatus Status { get; set; }


        public List<Book> Books { get; set; }
    }
}
