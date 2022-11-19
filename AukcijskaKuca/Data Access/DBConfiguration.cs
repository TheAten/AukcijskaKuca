using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AukcijskaKuca.Interfaces;

namespace AukcijskaKuca.Data_Access
{
    public static class DBConfiguration
    {
        public static IDBConnection connection { get; private set; }


        public static void InitializeConnection()
        {
            SQLConnector sqlConnector = new SQLConnector();
            connection = sqlConnector;
        }


        public static string CnnString(string name) => ConfigurationManager.ConnectionStrings[name].ConnectionString; 
    }
}
