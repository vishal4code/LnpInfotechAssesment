using Assessment.DAL.DbContext;
using Assessment.DAL.Entities;
using Assessment.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assessment.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // READ → Entity Framework
        public IEnumerable<Product> GetProducts(string search,
            decimal? minPrice, decimal? maxPrice, int? quantity, int page, int pageSize)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.ProductName.Contains(search));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice);

            if (quantity.HasValue)
                query = query.Where(p => p.Quantity == quantity);

            return query.Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
        }

        public int GetProductsCount(string search,
            decimal? minPrice, decimal? maxPrice, int? quantity)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p => p.ProductName.Contains(search));

            if (minPrice.HasValue)
                query = query.Where(p => p.Price >= minPrice);

            if (maxPrice.HasValue)
                query = query.Where(p => p.Price <= maxPrice);

            if (quantity.HasValue)
                query = query.Where(p => p.Quantity == quantity);

            return query.Count();
        }

        public Product GetById(int id) =>
            _context.Products.Find(id);

        // WRITE → Stored Procedures
        public void Add(Product p)
        {
            _context.Database.ExecuteSqlRaw(
                "EXEC sp_AddProduct @p0,@p1,@p2,@p3",
                p.ProductName, p.Description, p.Quantity, p.Price);
        }

        public void Update(Product p)
        {
            _context.Database.ExecuteSqlRaw(
                "EXEC sp_UpdateProduct @p0,@p1,@p2,@p3,@p4",
                p.ProductId, p.ProductName, p.Description, p.Quantity, p.Price);
        }

        public void Delete(int id)
        {
            _context.Database.ExecuteSqlRaw(
                "EXEC sp_DeleteProduct @p0", id);
        }
    }

}
