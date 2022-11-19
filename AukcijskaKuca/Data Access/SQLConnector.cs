using AukcijskaKuca.Interfaces;
using AukcijskaKuca.Model;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace AukcijskaKuca.Data_Access
{
    public class SQLConnector : IDBConnection
    {
        private readonly string db = "AuctionDB";

        public void CreateProduct(Product model)
        {
            using IDbConnection dbConnection = new SqlConnection(DBConfiguration.CnnString(db));
            var p = new DynamicParameters();
            p.Add("@Name", model.Name);
            p.Add("@Price", model.Price);
            p.Add("@LastOffer", 0);
            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            dbConnection.Open();
            dbConnection.Execute("dbo.spCreateProduct", p, commandType: CommandType.StoredProcedure);
            model.Id = p.Get<int>("@Id");
            dbConnection.Close();
        }

        public void DeleteProduct(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(DBConfiguration.CnnString(db));
            dbConnection.Open();
            dbConnection.Execute("dbo.spDeleteProduct", new { id }, commandType: CommandType.StoredProcedure);
            dbConnection.Close();
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                List<Product> products = new List<Product>();

                using IDbConnection dbConnection = new SqlConnection(DBConfiguration.CnnString(db));
                dbConnection.Open();
                products = dbConnection.Query<Product>("dbo.spGetAllProducts").ToList();
                dbConnection.Close();

                return products;
            }
            catch (SqlException)
            {
                MessageBox.Show("Something went wrong with DB");
                return null;
            }
        }
    }
}
