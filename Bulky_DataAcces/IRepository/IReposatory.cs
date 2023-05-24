using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ketab_DataAcces.IRepository
{
	public interface IRepository <T> where T : class
	{
		IEnumerable<T> GetAll ();
		T Get(Expression<Func<T, bool>> Filter);
		void add(T Entity );
		void update(T Entity);
		void Save();
		void remove(T Entity);

		void RemoveRange(IEnumerable<T> Entities);

	}
}
