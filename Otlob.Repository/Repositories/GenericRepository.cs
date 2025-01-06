using Microsoft.EntityFrameworkCore;
using Otlob.Core.IRepositories;
using Otlob.Core.Models;
using Otlob.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
       
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Product))
            {
                return await _context.Set<Product>()
                    .Include(p => p.ProductBrand)
                    .Include(p => p.ProductCategory)
                    .Cast<T>()
                    .ToListAsync();
            }
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id); 
        }
    }
}
