using Anas_sBookShelf.Dtos.Uploaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anas_sBookShelf.Dtos.CustomerDtos
{
    public class CustomerDto
    {

        public CustomerDto()
        {
            Images = new List<UploaderImageDto>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<UploaderImageDto> Images { get; set; }
    }
}
