using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Models.ViewModels
{

    public class OrderVM
    {
        public OrderHeader OrderHeader { get; set; }
        public IEnumerable<OrderDetail> OrderDetail { get; set; }
    }
}
