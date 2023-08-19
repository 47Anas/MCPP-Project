using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anas_sBookShelf.Dtos.OrderDtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Note { get; set; }

        public int CustomerId { get; set; }

        public List<int> BookIds { get; set; }
    }
}
