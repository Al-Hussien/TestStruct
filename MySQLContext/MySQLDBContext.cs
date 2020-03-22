using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MySQLContext
{
    public class MySQLDBContext :DbContext
    {
        public MySQLDBContext(DbContextOptions<MySQLDBContext> options)
           : base(options)
        {
        }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
    
}
