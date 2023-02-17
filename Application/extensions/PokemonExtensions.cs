using Application.ViewModels;
using Database.Models;

namespace Application.extensions;

public static class PokemonExtensions {
  public static PokemonModel ConvertToSave(this PokemonVM savePokemon) => new() {
    Name = savePokemon.Name,
    Image = savePokemon.Image,
    RegionId = savePokemon.RegionId,
    PrimaryTypeId = savePokemon.PrimaryTypeId,
    SecondaryTypeId = savePokemon.SecondaryTypeId,
    CreatedAt = DateTime.Now
  };

  public static PokemonModel ConvertToUpdate(this PokemonVM updatePokemon, PokemonModel pokemon) {
    pokemon.Name = updatePokemon.Name;
    pokemon.Image = updatePokemon.Image;
    pokemon.RegionId = updatePokemon.RegionId;
    pokemon.PrimaryTypeId = updatePokemon.PrimaryTypeId;
    pokemon.SecondaryTypeId = updatePokemon.SecondaryTypeId;
    pokemon.UpdatedAt = DateTime.Now;
    return pokemon;
  }

  public static PokemonVM ConvertToPokemonVM(this PokemonModel pokemon, string pokemonRegion, string PType, List<RegionVM> RegionsList, string SType, List<TypeVM> TypesList) => new() {
    Id = pokemon.Id,
    Name = pokemon.Name,
    Image = pokemon.Image,
    Region = pokemonRegion,
    PrimaryType = PType,
    SecondaryType = SType,
    Regions = RegionsList,
    Types = TypesList,
    RegionId = pokemon.RegionId,
    PrimaryTypeId = pokemon.PrimaryTypeId,
    SecondaryTypeId = pokemon.SecondaryTypeId,
    CreatedAt = pokemon.CreatedAt,
    UpdatedAt = pokemon.UpdatedAt
  };
}
