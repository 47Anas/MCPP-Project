using Anas_sBookShelf.Entities;

namespace Anas_sBookShelf.Dtos.CustomerDtos
{
    public class CustomerDetailsDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }

        public List<Cart> Carts { get; set; }
    }
}
