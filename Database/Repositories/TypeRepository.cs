using Database.Core;
using Database.Interfaces;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class TypeRepository : BaseRepository<TypeModel>, ITypeRepository {
  private readonly PokeContext _context;
  public TypeRepository(PokeContext context) : base(context) => _context = context;

  public async Task<TypeModel> GetEntity(int EntityId) => await this._context.Types.FindAsync(EntityId);

  async Task<IEnumerable<TypeModel>> IBaseRepository<TypeModel>.GetAll() => await this._context.Types.OrderByDescending(cb => cb.CreatedAt).ToListAsync();
  public async Task Save(TypeModel entity) {
    await this._context.Types.AddAsync(entity);
    await this._context.SaveChangesAsync();
  }
  public async Task Update(TypeModel entity) {
    this._context.Types.Update(entity);
    await this._context.SaveChangesAsync();
  }
  public async Task Remove(TypeModel entity) {
    this._context.Types.Remove(entity);
    await this._context.SaveChangesAsync();
  }
}
