using AukcijskaKuca.Interfaces;
using System.Configuration;

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
