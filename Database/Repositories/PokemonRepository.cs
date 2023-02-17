using Database.Core;
using Database.Interfaces;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories;

public class PokemonRepository : BaseRepository<PokemonModel>, IPokemonRepository {
  private readonly PokeContext _context;
  public PokemonRepository(PokeContext context) : base(context) => _context = context;

  public async Task<PokemonModel> GetEntity(int EntityId) => await this._context.Pokemons.FindAsync(EntityId);
  async Task<IEnumerable<PokemonModel>> IBaseRepository<PokemonModel>.GetAll() => await this._context.Pokemons.OrderByDescending(pk => pk.CreatedAt).ToListAsync();

  public async Task<IEnumerable<PokemonModel>> GetByRegion(int regionId) => await this._context.Pokemons.Where(pk => pk.RegionId == regionId).OrderByDescending(pk => pk.CreatedAt).ToListAsync();

  public async Task<IEnumerable<PokemonModel>> GetByType(int typeId) => await this._context.Pokemons.OrderByDescending(pk => pk.CreatedAt).Where(pk => pk.PrimaryTypeId == typeId || pk.SecondaryTypeId == typeId).ToListAsync();

  public async Task<IEnumerable<PokemonModel>> GetByName(string name) => await this._context.Pokemons.Where(pk => pk.Name.StartsWith(name)).OrderByDescending(pk => pk.CreatedAt).ToListAsync();

  public async Task Save(PokemonModel entity) {
    await this._context.Pokemons.AddAsync(entity);
    await this._context.SaveChangesAsync();
  }
  public async Task Update(PokemonModel entity) {
    this._context.Pokemons.Update(entity);
    await this._context.SaveChangesAsync();
  }
  public async Task Remove(PokemonModel entity) {
    this._context.Pokemons.Remove(entity);
    await this._context.SaveChangesAsync();
  }
}
