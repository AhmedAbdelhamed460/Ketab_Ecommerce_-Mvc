using Bulky_WebRazor_Temp.Data;
using Bulky_WebRazor_Temp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_WebRazor_Temp.Pages.Categores
{
    public class EditModel : PageModel
	{
		private readonly ApplicationDbcontex _db;
		[BindProperty]
		public Category Category { get; set; }
		public EditModel(ApplicationDbcontex db)
		{
			_db = db;
		}
		public void OnGet(int ? Id)
		{
			if (Id != null|| Id != 0)
			{
				Category=_db.categories.Find(Id);
			}
		}
		public IActionResult OnPost()
		{
			_db.categories.Update(Category);
			_db.SaveChanges();
			TempData["success"] = "category Updated successFully";
			return RedirectToPage("Index");
		}
	}
}
