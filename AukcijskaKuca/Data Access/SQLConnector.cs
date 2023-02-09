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

        public int AdminAccess(Users users)
        {
            using IDbConnection dbConnection = new SqlConnection(DBConfiguration.CnnString(db));

            var p = new DynamicParameters();
            p.Add("@Username", users.Username);
            dbConnection.Open();
            int i = (int)dbConnection.ExecuteScalar("dbo.spAdminCheck", p, commandType: CommandType.StoredProcedure);
            dbConnection.Close();
            return i;
        }

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
        } //

        public void CreateUser(Users users)
        {
            using IDbConnection dbConnetion = new SqlConnection(DBConfiguration.CnnString(db));
            var p = new DynamicParameters();
            p.Add("@Username", users.Username);
            p.Add("@Password", users.Password);
            p.Add("@Id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
            dbConnetion.Open();
            dbConnetion.Execute("dbo.spCreateUser", p, commandType: CommandType.StoredProcedure);
            users.Id = p.Get<int>("@Id");
            dbConnetion.Close();
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

        public int LoginCheck(Users users)
        {
            int result = 1;

            using IDbConnection dbConnection = new SqlConnection(DBConfiguration.CnnString(db));
            var p = new DynamicParameters();
            p.Add("@Username", users.Username);
            p.Add("@Password", users.Password);

            dbConnection.Open();
            result = (int)dbConnection.ExecuteScalar("dbo.spLoginCheck", p, commandType: CommandType.StoredProcedure);
            dbConnection.Close();
            return result;
        }

        public int RegisterCheck(Users users)
        {
            using IDbConnection dbConnection = new SqlConnection(DBConfiguration.CnnString(db));
            var p = new DynamicParameters();
            p.Add("@Username", users.Username);

            dbConnection.Open();
            int i = (int)dbConnection.ExecuteScalar("dbo.spRegisterCheck", p, commandType: CommandType.StoredProcedure);
            dbConnection.Close();
            return i;
        }

        public void UpdateProduct(Product model, int id)
        {
            using IDbConnection dbConnection = new SqlConnection(DBConfiguration.CnnString(db));
            var p = new DynamicParameters();
            p.Add("@Price", model.Price);
            p.Add("@id", model.Id);

            dbConnection.Open();
            dbConnection.Execute("dbo.spUpdateProduct", p, commandType: CommandType.StoredProcedure);
            dbConnection.Close();
        }
    }
}
