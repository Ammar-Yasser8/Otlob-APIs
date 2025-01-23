using Microsoft.EntityFrameworkCore;
using Otlob.Core.IRepositories;
using Otlob.Core.Models;
using Otlob.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Otlob.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;

        public GenericRepository(AppDbContext context)
        {
            _context = context;

        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? Predicate, string? Includes)
        {
            var query = _context.Set<T>().AsQueryable();
            if (Predicate != null)
            {
                query = query.Where(Predicate);
            }
            if (Includes != null)
            {
                foreach (var include in Includes.Split(','))
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }

        public Task<T?> GetAsync(Expression<Func<T, bool>>? Predicate, string? Includes)
        {
            var query = _context.Set<T>().AsQueryable();
            if (Predicate != null)
            {
                query = query.Where(Predicate);
            }
            if (Includes != null)
            {
                foreach (var include in Includes.Split(','))
                {
                    query = query.Include(include);
                }

            }
            return query.SingleOrDefaultAsync();
        }
    }
}
