using Bulky_WebRazor_Temp.Data;
using Bulky_WebRazor_Temp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bulky_WebRazor_Temp.Pages.Categores
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbcontex _db;
        public List<Category> Listcategories { get; set; }
        public IndexModel(ApplicationDbcontex db )
        {
            _db = db;
        }
        public void OnGet()
        {
			Listcategories = _db.categories.ToList();


		}
    }
}
