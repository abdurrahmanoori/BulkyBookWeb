using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        int IncreamentCount(ShoppingCart shoppingCart, int count);

        int DecreamentCount(ShoppingCart shoppingCart, int count);
    }
}
