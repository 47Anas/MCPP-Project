using Anas_sBookShelf.Dtos.CategoriesDtos;
using Anas_sBookShelf.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anas_sBookShelf.Dtos.BookDtos
{
    public class BookDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double Price { get; set; }

      //  public List<Cart> Carts { get; set; }

        public List<CategoryDto> Categories { get; set; }
    }
}

