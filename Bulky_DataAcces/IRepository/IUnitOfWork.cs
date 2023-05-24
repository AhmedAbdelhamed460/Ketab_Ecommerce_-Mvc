using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ketab_DataAcces.IRepository
{
	public interface IUnitOfWork
	{
		ICategoryReposatory category { get;  }
		IProductReposatory Product { get; }

	}
}
