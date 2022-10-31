using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace BioTekno.Task.Repositories.Abstract;

public interface IEntityRepositoryAsync<T> where T : class
{
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken,Expression<Func<T, bool>> filter = null);
    List<T> GetAll(Expression<Func<T, bool>> filter = null);
    Task<T> GetAsync(CancellationToken cancellationToken,Expression<Func<T, bool>> filter);
    Task<T> AddAsync(CancellationToken cancellationToken,T entity);
    Task<T> UpdateAsync(CancellationToken cancellationToken,T entity);
    Task<T> DeleteAsync(CancellationToken cancellationToken ,T entity);

}
