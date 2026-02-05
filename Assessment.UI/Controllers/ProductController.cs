using Assessment.BAL.Interfaces;
using Assessment.DAL.Entities;
using Assessment.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assessment.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        public IActionResult Index(string search,
            decimal? minPrice, decimal? maxPrice, int? quantity, int page = 1)
        {
            const int pageSize = 3;
            
            var products = _service.GetProducts(
                search, minPrice, maxPrice, quantity, page, pageSize);
            
            var totalCount = _service.GetProductsCount(
                search, minPrice, maxPrice, quantity);

            var viewModel = new ProductFilterVM
            {
                Products = products.Select(p => new ProductVM
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName,
                    Description = p.Description,
                    Quantity = p.Quantity,
                    Price = p.Price
                }),
                Search = search,
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                Quantity = quantity,
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = totalCount
            };

            return View(viewModel);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Product product)
        {
            _service.Add(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id) =>
            View(_service.GetById(id));

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            _service.Update(product);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
