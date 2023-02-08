using LymStyleWebAPPAPI.Domain.Contracts.Repositories;
using LymStyleWebAPPAPI.Domain.Contracts.Services;
using LymStyleWebAPPAPI.Domain.DTO;
using LymStyleWebAPPAPI.Domain.Entities;
using LymStyleWebAPPAPI.Web.Models.DTO;
using LymStyleWebAPPAPI.WebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LymStyleWebAPPAPI.WebMVC.Controllers
{

    public class ADMController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IAccountRepository _userRepository;
        private readonly IProductService _product;
        public ADMController(IProductRepository product, ICategoryRepository categoryRepository, IAccountRepository userRepository, IProductService productServ)
        {
            _productRepository = product;
            _categoryRepository = categoryRepository;
            _userRepository = userRepository;
            _product = productServ;
        }
        [Route("admin")]
        public async Task<IActionResult> Index()
        {
            var model = new AdmViewModel();
            model.product = GetAllProducts();
            model.category = GetAllCategories();
            model.user = GetAllUsers();
            return View(model);
        }
        [Route("admin/users")]
        public async Task<IActionResult> User()
        {
            var model = new AdmViewModel();
            model.user = GetAllUsers();
            return View(model);
        }


        #region Methods
        [HttpPost]
        public async Task<JsonResult> Delete(int? id)
        {
            var retDel = new ReturnJsonDel
            {
                status = "Success",
                code = "200"
            };
            try
            {
                if (await _product.Delete(id ?? 0) <= 0)
                {
                    retDel = new ReturnJsonDel
                    {
                        status = "Error",
                        code = "400"
                    };
                }
            }
            catch (Exception ex)
            {
                retDel = new ReturnJsonDel
                {
                    status = ex.Message,
                    code = "500"
                };
            }
            return Json(retDel);
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
        public List<AccountDTO> GetAllUsers()
        {
            return _userRepository.GetAll()
                             .Select(user => new AccountDTO()
                             {
                                 id = user.Id,
                                 name = user.Name,
                                 email = user.Email,
                                 password = user.Password,
                                 image = user.Image
                             }).ToList();
        }
        #endregion


    }
}
