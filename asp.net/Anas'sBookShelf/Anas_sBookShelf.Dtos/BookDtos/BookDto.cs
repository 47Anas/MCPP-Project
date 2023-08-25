using Anas_sBookShelf.Dtos.CategoriesDtos;
using Anas_sBookShelf.Dtos.Uploaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anas_sBookShelf.Dtos.BookDtos
{
    public class BookDto
    {
        public BookDto()
        {
            Images = new List<UploaderImageDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<UploaderImageDto> Images { get; set; }
    }
}
