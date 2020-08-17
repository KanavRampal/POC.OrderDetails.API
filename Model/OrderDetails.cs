using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.OrderDetails.API
{
    public class OrderDetails
    {
        public List<Order> OrderList { get; set; }
        public User User { get; set; }
    }
}
