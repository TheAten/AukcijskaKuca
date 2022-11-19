using AukcijskaKuca.Model;
using System.Collections.Generic;

namespace AukcijskaKuca.Interfaces
{
    public interface IDBConnection
    {
        void CreateProduct(Product model);
        void DeleteProduct(int id);

        List<Product> GetAllProducts();

    }
}
