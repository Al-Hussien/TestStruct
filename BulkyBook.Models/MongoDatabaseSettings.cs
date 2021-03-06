﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Models
{
    public class MongoDatabaseSettings : IMongoDatabaseSettings
    {
        //public string BooksCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IMongoDatabaseSettings
    {
        //string BooksCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
