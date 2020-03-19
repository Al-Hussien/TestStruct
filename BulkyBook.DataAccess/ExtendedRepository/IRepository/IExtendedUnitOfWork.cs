using SQLDbContext.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.ExtendedRepository.IRepository
{
    public interface IExtendedUnitOfWork : IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
    }
}
