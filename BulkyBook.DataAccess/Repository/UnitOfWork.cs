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
            ApplicationRepository = new ApplicationUserRepository(_db);
            ShoppingCartRepository = new ShoppingCartRepository(_db);
            
        }
        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public IProductRepository Product { get; private set; }
        public IApplicationRepository ApplicationRepository { get; private set; }
        public IShoppingCartRepository ShoppingCartRepository { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
