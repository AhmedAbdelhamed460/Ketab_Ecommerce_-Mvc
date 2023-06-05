
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
	public class CompanyController : Controller
	{
		private readonly IUnitOfWork _UnitOfWork;
		
		public CompanyController(IUnitOfWork db  )
		{

			_UnitOfWork = db;

		
		}
		public IActionResult Index()
		{
			
			var objCompany = _UnitOfWork.company.GetAll().ToList();
			

			return View(objCompany);
		}
		public IActionResult updateCreate(int?id)  // update and Create 
		{
			

			
			if (id == null || id == 0)
			{
				//Create
				return View(new Company());
			}
			else
			{
				// Update 
				Company Compobj= _UnitOfWork.company.Get(n => n.Id == id);
				return View(Compobj);
			}
		


			
		}
		[HttpPost]
		public IActionResult updateCreate(Company Companyobj)
		{
			
			if (ModelState.IsValid)
			{
				if(Companyobj.Id == 0)
				{
					_UnitOfWork.company.add(Companyobj);

				}
				else
				{
					_UnitOfWork.company.update(Companyobj);
				}

				_UnitOfWork.company.Save();
				TempData["success"] = "Company Create successFully";
				return RedirectToAction("index");
			}
			else
			{
               
				return View(Companyobj);
			}
			

		}



		#region Api Call

		[HttpGet]
		public IActionResult GetAll()
		{
			var objCompany = _UnitOfWork.company.GetAll().ToList();
			return Json(new { data = objCompany });
		}
		[HttpDelete]
		public IActionResult Delete(int id)
		{
			var objToDelete =_UnitOfWork.company.Get(u=>u.Id == id);

			if (objToDelete == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}
				
			_UnitOfWork.company.remove(objToDelete);
			_UnitOfWork.company.Save();
			return Json(new { success = true, message = "Deleting successful" });
		}
		#endregion
	}
}
