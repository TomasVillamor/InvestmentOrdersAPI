using investmentOrders.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace InvestmentOrdersAPI.DataAccess.Repositories.GenericRepository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DataAccessContext context;
    private readonly DbSet<T> dbSet;

    public GenericRepository(DataAccessContext context)
    {
        this.context = context;
        dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await dbSet.FindAsync(id);
        if (entity != null)
        {
            dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}