using AukcijskaKuca.Model;
using System.Collections.Generic;

namespace AukcijskaKuca.Interfaces
{
    public interface IDBConnection
    {
        void CreateProduct(Product model);
        void DeleteProduct(int id);

        void CreateUser(Users users);
        int AdminAccess(Users users);
        void UpdateProduct(Product model, int id);

        int LoginCheck(Users users);
        int RegisterCheck(Users users);

        List<Product> GetAllProducts();

    }
}
