using LymStyleWebAPPAPI.Domain.Contracts.Services;
using LymStyleWebAPPAPI.Domain.DTO;
using LymStyleWebAPPAPI.Web.Models.DTO;
using LymStyleWebAPPAPI.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace LymStyleWebAPPAPI.WebMVC.Controllers
{
   
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            // To List all categories
            // Get of CategoryRepository through Dependecy Injection (CategoryService)
            return View(_service.GetAll());
        }

        public JsonResult ListJson()
        {
            return Json(_service.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("id, name")] CategoryDTO category)
        {
            if (ModelState.IsValid)
            {
                if (await _service.Save(category) > 0)
                return RedirectToAction("Index", "Adm");
                
            }

            return View(category);
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _service.GetById(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("id, name")] CategoryDTO category)
        {
            if (!(id == category.id))
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (await _service.Save(category) > 0)
                    return RedirectToAction("Index", "Adm");
            }
            return View(category);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int? id)
        {
            var retDel = new ReturnJsonDel
            {
                status = "Success",
                code = "200"
            };
            if (await _service.Delete(id ?? 0) <= 0)
            {
                retDel = new ReturnJsonDel
                {
                    status = "Error",
                    code = "400"
                };
            }
            return Json(retDel);
        }
    }
}
