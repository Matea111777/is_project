using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Domain
{
    public class BooksInOrder : BaseEntity
    {
        public Guid BookId { get; set; }
        public Book? OrderedProduct { get; set; }

        public Guid OrderId { get; set; }
        public Orders? Order { get; set; }
        public int Quantity { get; set; }

    }
}
