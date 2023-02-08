using LymStyleWebAPPAPI.Application.Service.SQLServerServices;
using LymStyleWebAPPAPI.Domain.Contracts.Services;
using LymStyleWebAPPAPI.Domain.DTO;
using LymStyleWebAPPAPI.Web.Models.DTO;
using LymStyleWebAPPAPI.WebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace LymStyleWebAPPAPI.WebMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _user;

        public AccountController(IAccountService user)
        {
            _user = user;
        }
        public async Task<IActionResult> Index()
        {
            var list = _user.GetAll();
            return View(list);
        }
        
        public IActionResult Login()
        {

            return View();

        }

        public IActionResult Logout(AccountDTO user)
        {
            user.email = "";
            user.password = "";
            return RedirectToAction("Index", "Home");
        }


            [HttpPost]
        public IActionResult ValidLogin(AccountDTO user)
        {

            var loginList = _user.GetAll();
            AccountDTO result = loginList.FirstOrDefault();

            if (result.email == user.email && result.password == user.password)
            {
                return RedirectToAction("Index", "Adm");

            }
            else
            {
                TempData["Incorrect Login"] = "Invalid User";
                return RedirectToAction("Login", "Account");
            }
        }

        public JsonResult ListJson()
        {
            return Json(_user.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("id, name, email, password")] AccountDTO user)
        {
            if (ModelState.IsValid)
            {
                if (await _user.Save(user) > 0)
                    return RedirectToAction("Index", "Adm");

            }
           
            return View(user);
        }
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await _user.GetById(id);
            return View(category);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int? id, [Bind("id, name, email, password")] AccountDTO user)
        {
            if (!(id == user.id))
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (await _user.Save(user) > 0)
                    return RedirectToAction("Index", "Adm");
            }
            return View(user);
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
                if (await _user.Delete(id ?? 0) <= 0)
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
        public IActionResult ImagePost(int id)
        {
            ImageField productModel = new ImageField();
            if (id != null)
            {
                productModel.id = id;
            }
            return View(productModel);
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
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot//UserUploads", fileName);

                if (await _user.SaveFile(id, fileName) > 0)
                {
                    var stream = new FileStream(path, FileMode.Create);
                    file.CopyToAsync(stream);
                    return RedirectToAction("Index", "Adm");
                }
                ViewBag.Message = $"It's not possible to save the file: {path}";
                return View(new ImageField() { id = id, image = fileName });
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Error: {ex.Message}";
            }
            return View();
        }
    }
}

