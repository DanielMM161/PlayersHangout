namespace Backend.src.Repositories.BaseRepo;

using Backend.Src.Db;
using Backend.Src.Models;
using Microsoft.EntityFrameworkCore;

public class BaseRepo<T> : IBaseRepo<T>
    where T : BaseModel
{
    protected readonly AppDbContext _context;

    public BaseRepo(AppDbContext context)
    {
        _context = context;
    }

    public virtual async Task<T?> CreateOneAsync(T create)
    {
        await _context.AddAsync(create);
        await _context.SaveChangesAsync();        
        return create;
    }

    public async Task<bool> DeleteOneAsync(Guid id)
    {
        var item = _context.Set<T>().SingleOrDefault(model => model.Id == id);
        if (item != null)
        {
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;        
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(IFilterOptions? request)
    {
        var query = _context.Set<T>().AsNoTracking().Where(c => true);

        if (request is BaseQueryOptions filter)
        {
            return await query
                .Skip(filter.Skip)
                .Take(filter.Limit)
                .ToListAsync();
        }
        return await query.ToListAsync();      
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {        
        return await _context
            .Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(model => model.Id == id);
    }

    public async Task<T> UpdateOneAsync(T update)
    {        
        _context.Update<T>(update);
        await _context.SaveChangesAsync();
        return update;
    }
}