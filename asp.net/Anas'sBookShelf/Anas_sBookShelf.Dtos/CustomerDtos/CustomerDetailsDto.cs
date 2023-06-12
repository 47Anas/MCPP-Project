using Anas_sBookShelf.Entities;

namespace Anas_sBookShelf.Dtos.CustomerDtos
{
    public class CustomerDetailsDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<Cart> Carts { get; set; }
    }
}
