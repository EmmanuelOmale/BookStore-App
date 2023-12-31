﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingDomain.Entities.Dtos
{
    public class CartDto
    {
        public string BookId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public List<CartItemDto> OrderItems { get; set; }
    }
}
