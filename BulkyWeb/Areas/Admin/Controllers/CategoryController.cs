
using Ketab_DataAcces.Data;
using Ketab_DataAcces.IRepository;

using Ketab_Models;
using Microsoft.AspNetCore.Mvc;

namespace Ketab_Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        public CategoryController(IUnitOfWork db)
        {

            _UnitOfWork = db;
        }
        public IActionResult Index()
        {
            //IEnumerable<Category> Category =_db.categories.ToList();
            // IList<Category> Category = _db.categories.ToList();
            var objCategory = _UnitOfWork.category.GetAll().ToList();
            return View(objCategory);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (category.Name == category.DesplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "cannot Match Name and Display Order ");
            }
            if (ModelState.IsValid)
            {
                _UnitOfWork.category.add(category);
                _UnitOfWork.category.Save();
                TempData["success"] = "category Create successFully";
                return RedirectToAction("index");
            }
            return View();

        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objCategory = _UnitOfWork.category.Get(n => n.CategoryId == id);
            return View(objCategory);
        }
        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (ModelState.IsValid)
            {
                _UnitOfWork.category.update(category);
                _UnitOfWork.category.Save();
                TempData["success"] = "category Updated successFully";
                return RedirectToAction("index");
            }
            return View();

        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var objCategory = _UnitOfWork.category.Get(n => n.CategoryId == id);
            return View(objCategory);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            var objCategory = _UnitOfWork.category.Get(n => n.CategoryId == id);

            if (objCategory == null)
            {
                return NotFound();
            }
            _UnitOfWork.category.remove(objCategory);
            _UnitOfWork.category.Save();
            TempData["success"] = "category Deleted successFully";
            return RedirectToAction("index");



        }
    }
}
