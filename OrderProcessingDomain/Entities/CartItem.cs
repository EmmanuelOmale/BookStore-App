﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingDomain.Entities
{
    public class CartItem 
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string BookId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string CartId { get; set; }
    }
}
