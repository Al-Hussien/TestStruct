using System;
using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;

namespace SQLContext
{
    public class SQLDBContext: DbContext
    {
        public SQLDBContext(DbContextOptions<SQLDBContext> options)
           : base(options)
        {
        }

        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
