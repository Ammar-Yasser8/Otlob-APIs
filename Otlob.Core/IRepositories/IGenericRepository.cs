using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Otlob.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetAsync(Expression<Func<T,bool>>? Predicate = null , string ? Includes=null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? Predicate =null, string? Includes =null);
    }
}
