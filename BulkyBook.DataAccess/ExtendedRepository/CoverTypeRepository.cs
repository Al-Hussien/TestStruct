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
    public class CoverTypeRepository:Repository<CoverType>,ICoverTypeRepository
    {
        private readonly DbSet<CoverType> __dbSet;

        public CoverTypeRepository(DbSet<CoverType> __dbset) : base(__dbset)
        {
            //_dbContext = dbContext;
            __dbSet = __dbset;
        }

        public void Update(CoverType coverType)
        {
            var objFromDb = __dbSet.FirstOrDefault(s => s.Id == coverType.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = coverType.Name;
            }
        }
    }
}
