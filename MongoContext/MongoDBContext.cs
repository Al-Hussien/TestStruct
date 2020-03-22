using BulkyBook.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MongoContext
{
    public class MongoDBContext : DbContext
    {
        public MongoDBContext(DbContextOptions<MongoDBContext> options)
            : base(options)
        {

        }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
