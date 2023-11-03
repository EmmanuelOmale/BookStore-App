using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingDomain.Entities
{
    public class Enum
    {
        public enum OrderStatus
        {
            Pending,
            Processing,
            Sucessful,
            Shipped,
            Delivered,
            Unknown,
        }
    }

    
}
