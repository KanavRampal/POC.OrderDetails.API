using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.OrderDetails.API
{
    public class Order
    {
        public int OrderId { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
