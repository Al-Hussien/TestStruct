using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using SQLDbContext.DBContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class ExtendedUnitOfWork : IExtendedUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public ExtendedUnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Category = new CategoryRepository( _dbContext.Set<Category>());
            CoverType = new CoverTypeRepository(_dbContext.Set<CoverType>());
        }

        public ICoverTypeRepository CoverType { get; private set; }
        public ICategoryRepository Category { get; private set; }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
