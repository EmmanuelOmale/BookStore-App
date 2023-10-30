using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreDomain.Entities.Dtos
{
    public class AddBookDto
    {
        public string Title { get; set; } = "";
        public string Author { get; set; } = "";
        public string? ISBN { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Description { get; set; } = "";
        public DateTime PublicationDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
    }
}
