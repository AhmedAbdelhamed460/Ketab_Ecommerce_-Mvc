using Ketab_DataAcces.Data;
using Ketab_DataAcces.IRepository;
using Ketab_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ketab_DataAcces.Repository
{
	public class ProductRepositry : Repository<Product> ,IProductReposatory
	{
		private readonly ApplicationDbContext _db;

		public ProductRepositry(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}
	}
}
