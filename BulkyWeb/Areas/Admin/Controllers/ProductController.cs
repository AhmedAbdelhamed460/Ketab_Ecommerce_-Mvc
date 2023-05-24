
using Ketab_DataAcces.Data;
using Ketab_DataAcces.IRepository;

using Ketab_Models;
using Ketab_Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;

namespace Ketab_Web.Areas.Admin.Controllers
{
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _UnitOfWork;
		public ProductController(IUnitOfWork db)
		{

			_UnitOfWork = db;
		}
		public IActionResult Index()
		{
			
			var objProduct = _UnitOfWork.Product.GetAll().ToList();
			

			return View(objProduct);
		}
		public IActionResult Create()
		{
			IEnumerable<SelectListItem> CategoryList = _UnitOfWork.category.GetAll().ToList()
				.Select(n => new SelectListItem
				{
					Text = n.Name,
					Value = n.CategoryId.ToString(),
				});

			ProductVM productVM = new ProductVM()
			{
				CategoryList = CategoryList,

				Product = new Product() 
			};
		

			return View(productVM);
		}
		[HttpPost]
		public IActionResult Create (ProductVM obj)
		{
			
			if (ModelState.IsValid)
			{
				_UnitOfWork.Product.add(obj.Product);
				_UnitOfWork.Product.Save();
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
			var objProduct = _UnitOfWork.Product.Get(n => n.Id == id);
			return View(objProduct);
		}
		[HttpPost]
		public IActionResult Edit(Product Product)
		{

			if (ModelState.IsValid)
			{
				_UnitOfWork.Product.update(Product);
				_UnitOfWork.Product.Save();
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
			var objProduct = _UnitOfWork.Product.Get(n => n.Id == id);
			return View(objProduct);
		}
		[HttpPost, ActionName("Delete")]
		public IActionResult DeletePost(int? id)
		{
			var objProduct = _UnitOfWork.Product.Get(n => n.Id == id);

			if (objProduct == null)
			{
				return NotFound();
			}
			_UnitOfWork.Product.remove(objProduct);
			_UnitOfWork.Product.Save();
			TempData["success"] = "category Deleted successFully";
			return RedirectToAction("index");



		}
	}
}
