using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anas_sBookShelf.Dtos.BookDtos
{
    public class BookListDto
    {

        public string Name { get; set; }
        public double Price { get; set; }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
}
