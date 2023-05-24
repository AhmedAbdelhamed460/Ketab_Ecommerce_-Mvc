using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Ketab_Models
{
	public class Product
	{
        public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ISBN { get; set; }
		public string Author { get; set; }
		[Display(Name = "Price For 1-50 ")]
		[Range(1,1000)]
		public Double Price { get; set; }
		[Display( Name ="Price For 50+")]
		[Range(1, 1000)]
		public Double Price50 { get; set; }
		[Display(Name = "Price For 100+")]
		[Range(1, 1000)]
		public Double Price100 { get; set; }
		public int CategoryId { get; set; }
		[ValidateNever]
		public Category Category { get; set; }
        public string? ImageUrl { get; set; }



    }
}
