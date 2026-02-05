using Assessment.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.BAL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(string search,
            decimal? minPrice, decimal? maxPrice, int? quantity, int page, int pageSize);

        int GetProductsCount(string search,
            decimal? minPrice, decimal? maxPrice, int? quantity);

        Product GetById(int id);
        void Add(Product product);
        void Update(Product product);
        void Delete(int id);
    }

}
