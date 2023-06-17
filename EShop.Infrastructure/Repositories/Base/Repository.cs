using EShop.Domain.Entities.Common;
using EShop.Domain.Interfaces.Repositories.Base;
using EShop.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EShop.Infrastructure.Repositories.Base;

public class Repository<T> : IRepository<T>, IDisposable where T : Entity
{
    public Repository(ApplicationDbContext dbContext)
    {
        Context = dbContext;
    }

    private DbSet<T> entity;
    private string error = string.Empty;
    public ApplicationDbContext Context { get; set; }

    protected virtual DbSet<T> Entity
    {
        get => entity ?? (entity = Context.Set<T>());
    }

    public void Dispose()
    {
        if (Context != null)
        {
            Context.Dispose();
        }
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await Entity.ToListAsync();
    }

    public async Task<T> GetBy(Expression<Func<T, bool>> predicate)
    {
        return await Entity.FirstOrDefaultAsync(predicate);
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await Entity.Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
    {
        IQueryable<T> query = Entity;

        if (predicate != null)
            query = query.Where(predicate);

        if (!string.IsNullOrEmpty(includeString))
            query = query.Include(includeString);

        if (disableTracking)
            query = query.AsNoTracking();

        if (orderBy != null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = Entity;

        if (predicate != null)
            query = query.Where(predicate);

        if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

        if (disableTracking)
            query = query.AsNoTracking();

        if (orderBy != null)
            return await orderBy(query).ToListAsync();

        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await Entity.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        if (entity == null)
            throw new ArgumentNullException();

        
        await Entity.AddAsync(entity);
    }

    public void Update(T entity)
    {
        try
        {
            if (entity == null)
                throw new ArgumentNullException();

            Context.Entry(entity).State = EntityState.Modified;
        }
        catch (Exception ex)
        {
            error = ex.Message;
            throw new Exception(error);
        }
    }

    public void Delete(T entity)
    {
        try
        {
            if (entity == null)
            {
                throw new ArgumentNullException();
            }

            Entity.Remove(entity);
        }
        catch (Exception ex)
        {
            error = ex.Message;
            throw new Exception(error);
        }
    }
}
