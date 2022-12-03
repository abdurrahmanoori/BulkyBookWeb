using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        ICoverTypeRepository CoverType { get; }

        IProductRepository Product { get; }

        IApplicationRepository ApplicationRepository { get;  }

        IShoppingCartRepository ShoppingCart { get; }

        ICompanyRepository Company { get; }
        
        void Save();
        
        //ICompanyRepository Company { get; }
        //IApplicationUserRepository ApplicationUser { get; }
        //IShoppingCartRepository ShoppingCart { get; }
        //IOrderDetailRepository OrderDetail { get; }
        //IOrderHeaderRepository OrderHeader { get; }

    }
}
