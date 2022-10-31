using BioTekno.Task.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BioTekno.Task.Repositories.Concrete;

public class EfEntityRepositoryAsyncBase<TEntity, TContext> : IEntityRepositoryAsync<TEntity>
where TEntity : class
where TContext : DbContext
{
    protected TContext _context { get; }
    public EfEntityRepositoryAsyncBase(TContext context)
    {
        _context = context;
    }
    public async Task<TEntity> AddAsync(CancellationToken cancellationToken,TEntity entity)
    {

       // _context.Entry(entity).State = EntityState.Added;
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;

    }

    public async Task<TEntity> DeleteAsync(CancellationToken cancellationToken,TEntity entity)
    {
        //_context.Entry(entity).State = EntityState.Deleted;
         _context.Set<TEntity>().Remove(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;

    }

    public async Task<TEntity> GetAsync(CancellationToken cancellationToken,Expression<Func<TEntity, bool>> filter)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(filter, cancellationToken);
    }

    public async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken,Expression<Func<TEntity, bool>> filter = null)
    {

        return filter == null ? await _context.Set<TEntity>().ToListAsync(cancellationToken) : await _context.Set<TEntity>().Where(filter).ToListAsync(cancellationToken);

    }
    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
    {

        return filter == null ? _context.Set<TEntity>().ToList() : _context.Set<TEntity>().Where(filter).ToList();

    }

    public async Task<TEntity> UpdateAsync(CancellationToken cancellationToken,TEntity entity)
    {

       // _context.Entry(entity).State = EntityState.Modified;
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;

    }
}
