using BulkyBook.Models;
using Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.ExtendedRepository.IRepository
{
    public interface ICoverTypeRepository: IRepository<CoverType>
    {
        void Update(CoverType coverType);
    }
}
