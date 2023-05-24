using Bulky_WebRazor_Temp.Data;
using Bulky_WebRazor_Temp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_WebRazor_Temp.Pages.Categores
{
    public class CreateModel : PageModel
    {
		private readonly ApplicationDbcontex _db;
		[BindProperty]
		public  Category Category { get; set; }
		public CreateModel(ApplicationDbcontex db)
		{
			_db = db;
		}
		public void OnGet()
        {
        }
		public IActionResult OnPost()
		{
			_db.categories.Add(Category);
			_db.SaveChanges();
			TempData["success"] = "category Create successFully";
			return RedirectToPage("Index");
		}
    }
}
