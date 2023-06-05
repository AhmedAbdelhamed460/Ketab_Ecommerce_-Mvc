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
	public class ShoppingCartRepositry : Repository<ShoppingCart> , IShoppingCartRepository
    {
		private readonly ApplicationDbContext _db;

        public ShoppingCartRepositry(ApplicationDbContext db) : base(db)
        {
            
            _db = db;
        }




    }
}
