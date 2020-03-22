using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoContext;
using MySQLContext;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using SQLContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook
{
    public class InitDBType
    {
        private readonly IServiceCollection _services;
        private readonly IConfigurationSection _mongoSettings;
        private readonly string _connectionString;
        private readonly string _dbType;
        public InitDBType(IServiceCollection services, IConfiguration config)
        {
            _dbType = config.GetConnectionString("ConnectionUsed");
            _connectionString = config.GetConnectionString(_dbType + "Connection");
            _services = services;
            _mongoSettings = config.GetSection(nameof(MongoDatabaseSettings)); ;
        }
        public IDBConfigyration initDBType()
        {
            if (_dbType == "SQL")
            {
                return new SQLDBConfigyration(_services, _connectionString);
            }
            else if (_dbType == "MySQL")
            {
                return new MySQLDBConfigyration(_services, _connectionString);
            }
            else
            {
                return new MongoDBConfigyration(_services, _connectionString, _mongoSettings);
            }
        }
    } 
    public interface IDBConfigyration
    {
        public void AddCustomServices();
    }
    public class BaseDBConfig
    {
        protected readonly IServiceCollection _services;
        protected readonly string _connectionString;
        protected readonly IConfigurationSection _mongoConfig;
        public BaseDBConfig(IServiceCollection services, string connectionString, IConfigurationSection mongoConfig = null)
        {
            _services = services;
            _connectionString = connectionString;
            _mongoConfig = mongoConfig;
        }
    }
    public class SQLDBConfigyration : BaseDBConfig, IDBConfigyration
    {

        public SQLDBConfigyration(IServiceCollection services, string connectionString):base(services, connectionString){}
        public void AddCustomServices()
        {
            _services.AddScoped<DbContext, SQLDBContext>();

            _services.AddDbContext<SQLDBContext>(options =>
               options.UseSqlServer(_connectionString, sqlOptions =>
               {
                   sqlOptions.MigrationsAssembly("SQLContext");
               }));
            _services.AddDefaultIdentity<IdentityUser>()
               .AddEntityFrameworkStores<SQLDBContext>();

        }
    }
    public class MySQLDBConfigyration : BaseDBConfig, IDBConfigyration
    {
        public MySQLDBConfigyration(IServiceCollection services, string connectionString) : base(services, connectionString){}
        public void AddCustomServices(/*IConventionSetBuilder temp*/)
        {

            //temp.CreateConventionSet()
            //var conventionSet = SqlServerConventionSetBuilder.Build();
            //var modelBuilder = new ModelBuilder(conventionSet);
            _services.AddScoped<DbContext, MySQLDBContext>();
            _services.AddDbContext<MySQLDBContext>(options =>
                    options.UseMySql(_connectionString, 
                        mySqlOptions =>
                        {
                            mySqlOptions
                                .MigrationsAssembly("MySQLContext")
                                // replace with your Server Version and Type
                                .ServerVersion(new ServerVersion(new Version(8, 0, 19), ServerType.MySql))
                                .CharSetBehavior(CharSetBehavior.AppendToAllColumns)
                                .CharSet(CharSet.Utf8Mb4)
                                //.CharSet(CharSet.Latin1)
                                ;
                        }).EnableDetailedErrors());
            _services.AddDefaultIdentity<IdentityUser>()
              .AddEntityFrameworkStores<MySQLDBContext>();
        }
    }
    public class MongoDBConfigyration : BaseDBConfig, IDBConfigyration
    {
        public MongoDBConfigyration(IServiceCollection services, string connectionString, IConfigurationSection mongoConfig) : base(services, connectionString, mongoConfig) { }
        public void AddCustomServices()
        {
            _services.AddScoped<DbContext, MongoDBContext>();
            _services.Configure<MongoDatabaseSettings>(_mongoConfig);
            _services.AddSingleton<IMongoDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);
            _services.AddDefaultIdentity<IdentityUser>()
              .AddEntityFrameworkStores<MongoDBContext>();
            //services.AddScoped<IMongoDbContext, MongoDbContext>();
        }
    }
}
