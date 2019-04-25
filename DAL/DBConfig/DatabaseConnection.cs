using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DAL.DBConfig
{
    public class DatabaseConnection
    {
        public static IConfiguration ConnectionConfiguration
        {
            get
            {
                string diretorio = $"{Directory.GetParent(Directory.GetCurrentDirectory()).ToString()}\\DAL";
                IConfigurationRoot Configuration = new ConfigurationBuilder()
                    .SetBasePath(diretorio)
                    .AddJsonFile("appsettings.json")
                    .Build();
                return Configuration;
            }
        }
    }
}
