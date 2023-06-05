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
	public class UnitOfWork : IUnitOfWork
	{
		public ICategoryReposatory category { get; private set; }
        public ICompanyRepository  company { get; private set; }
        public IShoppingCartRepository ShoppingCart { get; private set; }

		public IApplicationUserRepository applicationUser { get; private set; }

        public IProductReposatory Product { get; private set; }

      
        private readonly ApplicationDbContext _db;
		public UnitOfWork(ApplicationDbContext db )
        {

			_db = db;
			category = new CategoryRepsitory(db);
			
			applicationUser =new ApplicationUserRepository(db);
	   	Product = new ProductRepositry(db);
            company = new ComanyRepository(db);
			ShoppingCart=new ShoppingCartRepositry(db);

		}

        public void save()
        {
           _db.SaveChanges();
        }
    }
}
