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
    public class ComanyRepository : Repository<Company> , ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public ComanyRepository( ApplicationDbContext db ) :base(db)
        {
            _db = db;
            
        }
    }
}
