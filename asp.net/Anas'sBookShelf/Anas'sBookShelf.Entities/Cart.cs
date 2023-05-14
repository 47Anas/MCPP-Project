using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<Book> Books { get; set; }
    }
}
