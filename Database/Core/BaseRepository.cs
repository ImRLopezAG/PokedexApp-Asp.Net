using Microsoft.EntityFrameworkCore;

namespace Database.Core;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class {
  private readonly PokeContext _context;
  private readonly DbSet<TEntity> _dbSet;
  private PokeContext context;
  protected BaseRepository(PokeContext context) => this.context = context;

  public virtual async Task<IEnumerable<TEntity>> GetAll() => await this._dbSet.ToListAsync();
  public virtual async Task<TEntity> GetEntity(int EntityId) => await this._dbSet.FindAsync(EntityId);
  public virtual async Task Remove(TEntity Entity){
    this._dbSet.Remove(Entity);
    await this._context.SaveChangesAsync();
  }
  public async virtual Task Save(TEntity Entity) {
    await this._dbSet.AddAsync(Entity);
    await this._context.SaveChangesAsync();
  }
  public virtual async Task Update(TEntity Entity) { 
    this._context.Entry(Entity).State = EntityState.Modified;
    this._dbSet.Update(Entity);
    await this._context.SaveChangesAsync();
  }
}