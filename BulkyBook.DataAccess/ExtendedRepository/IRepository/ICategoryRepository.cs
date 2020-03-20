using BulkyBook.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.ExtendedRepository.IRepository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        void Update(Category category);
    }
}
