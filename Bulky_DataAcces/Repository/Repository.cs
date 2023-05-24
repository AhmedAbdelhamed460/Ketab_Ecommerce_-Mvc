using Ketab_DataAcces.Data;
using Ketab_DataAcces.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ketab_DataAcces.Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _db;
		internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext db)
        {
			_db = db;
			this._dbSet = _db.Set<T>();
        }
        public void add(T Entity)
		{
			_dbSet.Add(Entity);
		}

		public T Get(Expression<Func<T, bool>> Filter)
		{
			IQueryable<T> quary = _dbSet;
			quary = quary.Where(Filter);
			return quary.FirstOrDefault();


		}

		public IEnumerable<T> GetAll()
		{
			IQueryable<T> quary = _dbSet;
			return quary.ToList();
		}

		public void remove(T Entity)
		{
			_dbSet.Remove(Entity);
		}

		public void RemoveRange(IEnumerable<T> Entities)
		{
			_dbSet.RemoveRange(Entities);
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public void update(T Entity)
		{
			_dbSet.Update(Entity);
		}
	}
}
