using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using SQLDbContext.DBContext;
using SQLDbContext.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    public class CoverTypeRepository:Repository<CoverType>,ICoverTypeRepository
    {
        //private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<CoverType> __dbSet;

        public CoverTypeRepository(/*ApplicationDbContext dbContext,*/DbSet<CoverType> __dbset) : base(/*dbContext,*/ __dbset)
        {
            //_dbContext = dbContext;
            __dbSet = __dbset;
        }

        public void Update(CoverType coverType)
        {
            //var objFromDb = _dbContext.CoverTypes.FirstOrDefault(s => s.Id == coverType.Id);
            var objFromDb = __dbSet.FirstOrDefault(s => s.Id == coverType.Id);
            //var objFromDb = _dbContext.GenericModels.FirstOrDefault(s => s.Id == coverType.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = coverType.Name;
            }
        }
    }
}
