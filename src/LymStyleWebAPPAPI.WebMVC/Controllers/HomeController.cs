using LymStyleWebAPPAPI.Domain.Contracts.Repositories;
using LymStyleWebAPPAPI.Domain.DTO;
using LymStyleWebAPPAPI.WebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LymStyleWebAPPAPI.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ILogger<HomeController> logger,IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            var model = new AdmViewModel();
            model.product = GetAllProducts();
            model.category = GetAllCategories();
            return View(model);
        }
        public List<ProductDTO> GetAllProducts()
        {
            return _productRepository.GetAll()
                             .Select(product => new ProductDTO()
                             {
                                 id = product.Id,
                                 name = product.Name,
                                 categoryId = product.CategoryId,
                                 description = product.Description,
                                 price = product.Price,
                                 image = product.Image
                             }).ToList();
        }
        public List<CategoryDTO> GetAllCategories()
        {
            return _categoryRepository.GetAll()
                             .Select(category => new CategoryDTO()
                             {
                                 id = category.Id,
                                 name = category.Name,
                             }).ToList();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}