﻿//using System;
//using System.Collections.Generic;
//using System.Text;
//using BulkyBook.Models;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;

//namespace BulkyBook.DataAccess.Data
//{
//    public partial class ApplicationDbContext : DbContextParent
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<CoverType> CoverTypes { get; set; }
//        public DbSet<Category> Categories { get; set; }
//        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
//    }

//    public class DbContextParent:DbContext
//    {
//        public DbContextClass(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {

//        }
//    }
//}
