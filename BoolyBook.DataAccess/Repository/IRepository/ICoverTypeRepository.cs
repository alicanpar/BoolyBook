using BoolyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoolyBook.DataAccess.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType obj);
        //void Save();
        //IEnumerable<Category> GetAll();
        //Category GetFirstOrDefault(Expression<Func<Category, bool>> filter);
        //void Remove(Category entitiy);
        //void Save();
    }
}
