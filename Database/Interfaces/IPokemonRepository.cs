using Database.Core;
using Database.Models;
namespace Database.Interfaces;

public interface IPokemonRepository : IBaseRepository<PokemonModel> {
  Task<IEnumerable<PokemonModel>> GetByType(int typeId);
  Task<IEnumerable<PokemonModel>> GetByRegion(int regionId);
  Task<IEnumerable<PokemonModel>> GetByName(string name);
}
