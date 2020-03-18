using System;
using System.Collections.Generic;
using System.Text;
using BulkyBook.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SQLDbContext.DBContext;

namespace BulkyBook.DataAccess.Data
{
    public partial class ApplicationDbContext : SQLContext
    {
        public ApplicationDbContext(DbContextOptions<SQLContext> options)
            : base(options)
        {
        }
        public DbSet<CoverType> CoverTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
