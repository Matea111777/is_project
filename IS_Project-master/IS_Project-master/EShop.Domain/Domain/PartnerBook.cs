using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Domain
{
    public class PartnerBook : BaseEntity
    {
        public string? BookName { get; set; }
        public string? BookDescription { get; set; }
        public string? BookImage { get; set; }

        public int Price { get; set; }

        public int Rating { get; set; }
        public Guid AuthorId { get; set; }
        public PartnerAuthor? Author { get; set; }

    }
}
