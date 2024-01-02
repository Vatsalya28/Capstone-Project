﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace PayExpert.Utility
{
    internal static class DbConnUtil

    {
        private static IConfiguration _iconfiguration;
        static DbConnUtil()
        {
            GetAppSettingsFile();
        }
        private static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json");
            _iconfiguration=builder.Build();
        }
        public static string GetConnectionString()
        {
            return _iconfiguration.GetConnectionString("LocalConnectionString");
        }
        
    }
}
