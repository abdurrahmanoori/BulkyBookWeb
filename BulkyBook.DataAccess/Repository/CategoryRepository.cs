using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        public void Update(Category obj)
        {
            _db.Category.Update(obj);
        }
    }
}
