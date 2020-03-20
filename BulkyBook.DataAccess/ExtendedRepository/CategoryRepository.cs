using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.ExtendedRepository.IRepository;
using BulkyBook.Models;
using EFRepository.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.ExtendedRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        //private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<Category> __dbSet;
        public CategoryRepository(DbSet<Category> __dbset): base(/*dbContext,*/ __dbset)
        {
            __dbSet = __dbset;
        }

        public void Update(Category category)
        {
            var objFromDb = __dbSet.FirstOrDefault(s => s.Id == category.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = category.Name;
            }
        }
    }
}
