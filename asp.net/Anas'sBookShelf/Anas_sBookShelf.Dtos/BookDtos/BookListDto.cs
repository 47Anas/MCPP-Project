using Anas_sBookShelf.Dtos.CategoriesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anas_sBookShelf.Dtos.BookDtos
{
    public class BookListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public List<CategoryDto> Categories { get; set; }
    }
}
