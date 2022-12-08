using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Text;


namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            Company = new CompanyRepository(_db);
            OrderDetailRepository = new OrderDetailRepository(_db);
            OrderHeaderRepository = new OrderHeaderRepository(_db);

        }
        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public IProductRepository Product { get; private set; }

        public IApplicationRepository ApplicationUser { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public ICompanyRepository Company { get; private set; }

        public IOrderDetailRepository OrderDetailRepository { get; private set; }

        public IOrderHeaderRepository OrderHeaderRepository { get; private set; }


        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
