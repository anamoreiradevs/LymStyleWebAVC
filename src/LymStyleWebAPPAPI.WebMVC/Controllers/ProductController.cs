using LymStyleWebAPPAPI.Domain.Contracts.Services;
using LymStyleWebAPPAPI.Domain.DTO;
using LymStyleWebAPPAPI.Domain.Entities;
using LymStyleWebAPPAPI.Web.Models.DTO;
using LymStyleWebAPPAPI.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace LymStyleWebAPPAPI.WebMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService service, ICategoryService categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }
        
        
        public JsonResult ListJson()
        {
            return Json(_service.GetAll());
        }

        public IActionResult Create()
        {
            ViewData["categoryId"] = new SelectList(_categoryService.GetAll(), "id", "name", "Select...");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("id, categoryId, name, description, price")]ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                if(await _service.Save(product) > 0)
                    return RedirectToAction("Index", "Adm");
            }
            ViewData["categoryId"] = new SelectList(_categoryService.GetAll(), "id", "name", product.categoryId);
            return View(product);
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _service.GetById(id);
            ViewData["categoryId"] = new SelectList(_categoryService.GetAll(), "id", "name", product.categoryId);
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("id, categoryId, name, description, price")] ProductDTO product)
        {
            if (!(id == product.id))
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (await _service.Save(product) > 0)
                    return RedirectToAction("Index", "Adm");
            }
            return View(product);
        }

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
                if (await _service.Delete(id ?? 0) <= 0)
                {
                    retDel = new ReturnJsonDel
                    {
                        status = "Error",
                        code = "400"
                    };
                }
            } catch (Exception ex)
            {
                retDel = new ReturnJsonDel
                {
                    status = ex.Message,
                    code = "500"
                };
            }
            return Json(retDel);
        }
        public IActionResult ImagePost(int id)
        {
            ImageField userModel = new ImageField();
            if (id != null)
            {
                userModel.id = id;
            }
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> ImagePost(int id, List<IFormFile> image)
        {
            try
            {
                if (id == null)
                {
                    ViewBag.Message = $"ID is null";
                    return View(new ImageField() { id = id });
                }

                var file = image.FirstOrDefault();
                var fileName = $"{id}_{file.FileName}";
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//Uploads", fileName);

                if (await _service.SaveFile(id, fileName) > 0)
                {
                    var stream = new FileStream(path, FileMode.Create);
                    file.CopyToAsync(stream);
                    return RedirectToAction("Index", "Adm");
                }
                ViewBag.Message = $"It's not possible to save the file: {path}";
                return View(new ImageField() { id = id, image = fileName});
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
            }
            return View();
        }
    }
}