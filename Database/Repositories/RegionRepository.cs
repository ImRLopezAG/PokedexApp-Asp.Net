using Database.Core;
using Database.Interfaces;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class RegionRepository : BaseRepository<RegionModel>, IRegionRepository {
  private readonly PokeContext _context;
  public RegionRepository(PokeContext context) : base(context) => _context = context;

  public async Task<RegionModel> GetEntity(int EntityId) => await this._context.Regions.FindAsync(EntityId);
  async Task<IEnumerable<RegionModel>> IBaseRepository<RegionModel>.GetAll() => await this._context.Regions.OrderByDescending(cb => cb.CreatedAt).ToListAsync();
  public async Task Save(RegionModel entity) {
    await this._context.Regions.AddAsync(entity);
    await this._context.SaveChangesAsync();
  }

  public async Task Update(RegionModel entity) {
    this._context.Regions.Update(entity);
    await this._context.SaveChangesAsync();
  }
  public async Task Remove(RegionModel entity) {
    this._context.Regions.Remove(entity);
    await this._context.SaveChangesAsync();
  }
}
