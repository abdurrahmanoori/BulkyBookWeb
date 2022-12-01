using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; }
        //public double CartTotal { get; set; } - This now we can use directly

        public OrderHeader OrderHeader { get; set; }
    }
}
