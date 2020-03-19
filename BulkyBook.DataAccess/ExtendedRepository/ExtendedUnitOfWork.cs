using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.ExtendedRepository.IRepository;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.ExtendedRepository
{
    public class ExtendedUnitOfWork : IExtendedUnitOfWork
    {
        //if wanted to exclude it from Service Injector
        //private readonly ApplicationDbContext _dbContext = new ApplicationDbContext(((new DbContextOptionsBuilder<ApplicationDbContext>()).UseSqlServer("Server=DESKTOP-CPQGMJJ;Database=BulkyBook;Trusted_Connection=True;MultipleActiveResultSets=true")).Options);
        private readonly ApplicationDbContext _dbContext ;

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
