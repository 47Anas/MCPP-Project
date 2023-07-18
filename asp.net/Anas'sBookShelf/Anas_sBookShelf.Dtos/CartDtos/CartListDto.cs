using AnassBookShelf.Utils.Enums;

namespace Anas_sBookShelf.Dtos.CartDtos
{
    public class CartListDto
    {
        public int Id { get; set; }
        public string CustomerFullName { get; set; }
        public string TotalPrice { get; set; }
        public DateTime CheckOutDate { get; set; }
        public CartStatus Status { get; set; }
    }
}
