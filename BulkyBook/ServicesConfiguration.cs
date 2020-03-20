using BulkyBook.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
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
            _services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(_connectionString));
            
        }
    }
    public class MySQLDBConfigyration : BaseDBConfig, IDBConfigyration
    {
        public MySQLDBConfigyration(IServiceCollection services, string connectionString) : base(services, connectionString){}
        public void AddCustomServices()
        {
                _services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseMySql(_connectionString));
        }
    }
    public class MongoDBConfigyration : BaseDBConfig, IDBConfigyration
    {
        public MongoDBConfigyration(IServiceCollection services, string connectionString, IConfigurationSection mongoConfig) : base(services, connectionString, mongoConfig) { }
        public void AddCustomServices()
        {
            _services.Configure<MongoDatabaseSettings>(_mongoConfig);
            _services.AddSingleton<IMongoDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDatabaseSettings>>().Value);
            //services.AddScoped<IMongoDbContext, MongoDbContext>();
        }
    }
}
