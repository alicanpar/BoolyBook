using BoolyBook.DataAccess.IRepository;
using BoolyBook2.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BoolyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            //_db.Products.Include(u => u.Category).Include(u=>u.CoverType);
            this.dbSet=_db.Set<T>();
        }
        public void Add(T entitiy)
        {
            dbSet.Add(entitiy);
        }
        //includeProp-"Category, CoverType"
        public IEnumerable<T> GetAll(string? includeProperies = null)
        {
            IQueryable<T> query = dbSet;
            if(includeProperies != null)
            {
                foreach(var includeProp in includeProperies.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperies = null)
        {
            IQueryable<T> query = dbSet;
            query=query.Where(filter);
            if (includeProperies != null)
            {
                foreach (var includeProp in includeProperies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public void Remove(T entitiy)
        {
            dbSet.Remove(entitiy);
        }

        public void RemoveRange(IEnumerable<T> entitiy)
        {
            dbSet.RemoveRange(entitiy);
        }
    }
}
