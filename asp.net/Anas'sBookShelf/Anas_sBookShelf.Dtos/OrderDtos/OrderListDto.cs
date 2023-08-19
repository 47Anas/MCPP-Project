using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anas_sBookShelf.Dtos.OrderDtos
{
    public class OrderListDto
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public string Note { get; set; }
        public DateTime OrderDate { get; set; }

        public string CustomerFullName { get; set; }

    }
}
