using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using SQLDbContext.DBContext;
using SQLDbContext.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public CategoryRepository(ApplicationDbContext dbContext): base(dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(Category category)
        {
            //var objFromDb = _dbContext.Categories.FirstOrDefault(s => s.Id == category.Id);
            //var objFromDb = _dbContext.GenericModels.FirstOrDefault(s => s.Id == category.Id);
            var tempo = _dbContext.GetCollection<Category>();
            var objFromDb = tempo.FirstOrDefault(s => s.Id == category.Id);

            if (objFromDb != null)
            {
                objFromDb.Name = category.Name;
            }
        }
    }
}
