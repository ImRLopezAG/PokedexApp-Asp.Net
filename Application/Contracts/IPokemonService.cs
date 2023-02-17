using Application.Core;
using Application.ViewModels;

namespace Application.Contracts;

public interface IPokemonService : IBaseService {
  Task Save(PokemonVM savePokemon);
  Task Edit(PokemonVM editPokemon);
  Task Delete(int deletePokemon);

  Task<ServiceResult> GetByType(FilterVM pokemonType);

  Task<ServiceResult> GetByRegion(FilterVM pokemonRegion);

  Task<ServiceResult> GetByName(FilterVM pokemonName);
}
