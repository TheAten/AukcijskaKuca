using AukcijskaKuca.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AukcijskaKuca.Interfaces
{
    public interface IDBConnection
    {
        void CreateProduct(Product model);

        List<Product> GetAllProducts();

    }
}
