using Assessment.BAL.Interfaces;
using Assessment.DAL.Entities;
using Assessment.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.BAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;

        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Product> GetProducts(string search,
            decimal? minPrice, decimal? maxPrice, int? quantity, int page, int pageSize)
            => _repo.GetProducts(search, minPrice, maxPrice, quantity, page, pageSize);

        public int GetProductsCount(string search,
            decimal? minPrice, decimal? maxPrice, int? quantity)
            => _repo.GetProductsCount(search, minPrice, maxPrice, quantity);

        public Product GetById(int id) => _repo.GetById(id);
        public void Add(Product p) => _repo.Add(p);
        public void Update(Product p) => _repo.Update(p);
        public void Delete(int id) => _repo.Delete(id);
    }

}
