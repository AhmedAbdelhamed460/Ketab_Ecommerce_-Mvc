
using Ketab_DataAcces.Data;
using Ketab_DataAcces.IRepository;
using System.IO;
using Ketab_Models;
using Ketab_Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Ketab_Utility;

namespace Ketab_Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	//[Authorize( Roles =SD.Role_Admin)]
	public class ProductController : Controller
	{
		private readonly IUnitOfWork _UnitOfWork;
		private readonly  IWebHostEnvironment _webHostEnvironment;
		public ProductController(IUnitOfWork db , IWebHostEnvironment webHostEnvironment )
		{

			_UnitOfWork = db;

			_webHostEnvironment = webHostEnvironment;
		}
		public IActionResult Index()
		{
			
			var objProduct = _UnitOfWork.Product.GetAll( IncludeProp : "Category").ToList();
			

			return View(objProduct);
		}
		public IActionResult updateCreate(int?id)  // update and Create 
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
			if (id == null || id == 0)
			{
				//Create
				return View(productVM);
			}
			else
			{
				// Update 
				productVM.Product= _UnitOfWork.Product.Get(n => n.Id == id);
				return View(productVM);
			}
		


			
		}
		[HttpPost]
		public IActionResult updateCreate(ProductVM obj ,IFormFile? File)
		{
			
			if (ModelState.IsValid)
			{



				string wwwrootPath = _webHostEnvironment.WebRootPath;
				if (File != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName);
					string productPath = Path.Combine(wwwrootPath, @"images\Product");
					if(!string.IsNullOrEmpty(obj.Product.ImageUrl))
					{
						//Delete 
						string OldImagePath = Path.Combine(wwwrootPath , obj.Product.ImageUrl.TrimStart('\\'));
						if(System.IO.File.Exists(OldImagePath))
						{
							System.IO.File.Delete(OldImagePath);
						}
					}








					using (var FileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						File.CopyTo(FileStream);
					}
					obj.Product.ImageUrl = @"\images\Product\" + fileName;





				}
				if(obj.Product.Id == 0)
				{
					_UnitOfWork.Product.add(obj.Product);

				}
				else
				{
					_UnitOfWork.Product.update(obj.Product);
				}

				_UnitOfWork.Product.Save();
				TempData["success"] = "category Create successFully";
				return RedirectToAction("index");
			}
			else
			{
				obj.CategoryList = _UnitOfWork.category.GetAll().Select(s => new SelectListItem
				{

					Text = s.Name,
					Value = s.CategoryId.ToString(),
				});
				return View(obj);
			}
			

		}

		//public IActionResult Edit(int? id)
		//{
		//	if (id == null || id == 0)
		//	{
		//		return NotFound();
		//	}
		//	var objProduct = _UnitOfWork.Product.Get(n => n.Id == id);
		//	return View(objProduct);
		//}
		//[HttpPost]
		//public IActionResult Edit(Product Product)
		//{

		//	if (ModelState.IsValid)
		//	{
		//		_UnitOfWork.Product.update(Product);
		//		_UnitOfWork.Product.Save();
		//		TempData["success"] = "category Updated successFully";
		//		return RedirectToAction("index");
		//	}
		//	return View();

		//}


		//public IActionResult Delete(int? id)
		//{
		//	if (id == null || id == 0)
		//	{
		//		return NotFound();
		//	}
		//	var objProduct = _UnitOfWork.Product.Get(n => n.Id == id);
		//	return View(objProduct);
		//}
		//[HttpPost, ActionName("Delete")]
		//public IActionResult DeletePost(int? id)
		//{
		//	var objProduct = _UnitOfWork.Product.Get(n => n.Id == id);

		//	if (objProduct == null)
		//	{
		//		return NotFound();
		//	}
		//	_UnitOfWork.Product.remove(objProduct);
		//	_UnitOfWork.Product.Save();
		//	TempData["success"] = "category Deleted successFully";
		//	return RedirectToAction("index");



		//}


		#region Api Call

		[HttpGet]
		public IActionResult GetAll()
		{
			var objProduct = _UnitOfWork.Product.GetAll(IncludeProp: "Category").ToList();
			return Json(new { data = objProduct });
		}
		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var objToDelete =_UnitOfWork.Product.Get(u=>u.Id == id);

			if (objToDelete == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}
				var OldPathImage =  
					Path.Combine(_webHostEnvironment.WebRootPath,
					objToDelete.ImageUrl.TrimStart('\\'));

				if (System.IO.File.Exists(OldPathImage))
				{
					System.IO.File.Delete(OldPathImage);
				}

			_UnitOfWork.Product.remove(objToDelete);
			_UnitOfWork.Product.Save();
			return Json(new { success = true, message = "Deleting successful" });
		}
		#endregion
	}
}
