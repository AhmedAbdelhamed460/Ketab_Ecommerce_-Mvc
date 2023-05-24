using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bulky_WebRazor_Temp.Model
{
	
		public class Category
		{
			public int CategoryId { get; set; }
			[DisplayName("Category Name ")]
			public string Name { get; set; }
			[DisplayName("Display Order")]
			[Range(1, 100)]
			public int DesplayOrder { get; set; }
		}
	
}
