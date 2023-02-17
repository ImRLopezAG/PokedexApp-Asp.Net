using Application.Contracts;
using Application.Core;
using Application.extensions;
using Application.Validations;
using Application.ViewModels;
using Database.Interfaces;
using Database.Models;

namespace Application.Services;

public class PokemonService : IPokemonService {
  private readonly IPokemonRepository _pokemonRepository;
  private readonly IRegionRepository _regionRepository;
  private readonly ITypeRepository _typeRepository;

  private readonly ILoggerService<PokemonService> _loggerService;

  public PokemonService(IPokemonRepository pokemonRepository, IRegionRepository regionRepository, ITypeRepository typeRepository, ILoggerService<PokemonService> loggerService) {
    _pokemonRepository = pokemonRepository;
    _regionRepository = regionRepository;
    _typeRepository = typeRepository;
    _loggerService = loggerService;
  }
  public async Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var query = from pokemon in await _pokemonRepository.GetAll()
                  select pokemon.ConvertToPokemonVM(
                    _regionRepository.GetEntity(pokemon.RegionId).ContinueWith(x => x.Result.Name).Result,
                    _typeRepository.GetEntity(pokemon.PrimaryTypeId).ContinueWith(x => x.Result.Name).Result,
                    _regionRepository.GetAll().ContinueWith(x => x.Result.Select(y => new RegionVM() { Id = y.Id, Name = y.Name }).ToList()).Result,
                    pokemon.SecondaryTypeId != null ? _typeRepository.GetEntity(pokemon.SecondaryTypeId.Value).ContinueWith(x => x.Result.Name).Result : null,
                    _typeRepository.GetAll().ContinueWith(x => x.Result.Select(y => new TypeVM() { Id = y.Id, Name = y.Name }).ToList()).Result
                  );
      result.Data = query.ToList();
    } catch (Exception ex) {
      result.Message = "An error occurred while getting the pokemons";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
    return result;
  }

  public async Task<ServiceResult> GetById(int id) {
    ServiceResult result = new();
    try {
      PokemonModel pokemon = await _pokemonRepository.GetEntity(id);
      if (pokemon != null) {
        result.Data = pokemon.ConvertToPokemonVM(
          _regionRepository.GetEntity(pokemon.RegionId).ContinueWith(x => x.Result.Name).Result,
          _typeRepository.GetEntity(pokemon.PrimaryTypeId).ContinueWith(x => x.Result.Name).Result,
          _regionRepository.GetAll().ContinueWith(x => x.Result.Select(y => new RegionVM() { Id = y.Id, Name = y.Name }).ToList()).Result,
          pokemon.SecondaryTypeId != null ? _typeRepository.GetEntity(pokemon.SecondaryTypeId.Value).ContinueWith(x => x.Result.Name).Result : null,
          _typeRepository.GetAll().ContinueWith(x => x.Result.Select(y => new TypeVM() { Id = y.Id, Name = y.Name }).ToList()).Result
        );
      } else {
        result.Message = "Pokemon not found";
        result.Success = false;
      }
    } catch (Exception ex) {
      result.Message = "An error occurred while getting the pokemon";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
    return result;
  }

  public async Task<ServiceResult> GetByType(FilterVM filter) {
    ServiceResult result = new();
    try {
      var query = from pokemon in await _pokemonRepository.GetByType(filter.TypeId)
                  select pokemon.ConvertToPokemonVM(
                    _regionRepository.GetEntity(pokemon.RegionId).ContinueWith(x => x.Result.Name).Result,
                    _typeRepository.GetEntity(pokemon.PrimaryTypeId).ContinueWith(x => x.Result.Name).Result,
                    _regionRepository.GetAll().ContinueWith(x => x.Result.Select(y => new RegionVM() { Id = y.Id, Name = y.Name }).ToList()).Result,
                    pokemon.SecondaryTypeId != null ? _typeRepository.GetEntity(pokemon.SecondaryTypeId.Value).ContinueWith(x => x.Result.Name).Result : null,
                    _typeRepository.GetAll().ContinueWith(x => x.Result.Select(y => new TypeVM() { Id = y.Id, Name = y.Name }).ToList()).Result
                  );
      result.Data = query.ToList();
    } catch (Exception ex) {
      result.Message = "An error occurred while getting the pokemons";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
    return result;
  }

  public async Task<ServiceResult> GetByRegion(FilterVM pokemonRegion) {
    ServiceResult result = new();
    try {
      var query = from pokemon in await _pokemonRepository.GetByRegion(pokemonRegion.RegionId)
                  select pokemon.ConvertToPokemonVM(
                    _regionRepository.GetEntity(pokemon.RegionId).ContinueWith(x => x.Result.Name).Result,
                    _typeRepository.GetEntity(pokemon.PrimaryTypeId).ContinueWith(x => x.Result.Name).Result,
                    _regionRepository.GetAll().ContinueWith(x => x.Result.Select(y => new RegionVM() { Id = y.Id, Name = y.Name }).ToList()).Result,
                    pokemon.SecondaryTypeId != null ? _typeRepository.GetEntity(pokemon.SecondaryTypeId.Value).ContinueWith(x => x.Result.Name).Result : null,
                    _typeRepository.GetAll().ContinueWith(x => x.Result.Select(y => new TypeVM() { Id = y.Id, Name = y.Name }).ToList()).Result
                  );
      result.Data = query.ToList();
    } catch (Exception ex) {
      result.Message = "An error occurred while getting the pokemons";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
    return result;
  }

  public async Task<ServiceResult> GetByName(FilterVM filter) {
    ServiceResult result = new();
    try {
      var query = from pokemon in await _pokemonRepository.GetByName(filter.Name)
                  select pokemon.ConvertToPokemonVM(
                    _regionRepository.GetEntity(pokemon.RegionId).ContinueWith(x => x.Result.Name).Result,
                    _typeRepository.GetEntity(pokemon.PrimaryTypeId).ContinueWith(x => x.Result.Name).Result,
                    _regionRepository.GetAll().ContinueWith(x => x.Result.Select(y => new RegionVM() { Id = y.Id, Name = y.Name }).ToList()).Result,
                    pokemon.SecondaryTypeId != null ? _typeRepository.GetEntity(pokemon.SecondaryTypeId.Value).ContinueWith(x => x.Result.Name).Result : null,
                    _typeRepository.GetAll().ContinueWith(x => x.Result.Select(y => new TypeVM() { Id = y.Id, Name = y.Name }).ToList()).Result
                  );
      result.Data = query.ToList();
    } catch (Exception ex) {
      result.Message = "An error occurred while getting the pokemons";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
    return result;
  }

  public async Task Save(PokemonVM savePokemon) {
    ServiceResult result = new();
    try {
      var isValidPokemon = ValidatePokemon.IsValidPokemon(savePokemon);
      if (isValidPokemon.Success) {
        PokemonModel pokemon = savePokemon.ConvertToSave();
        await _pokemonRepository.Save(pokemon);
      } else {
        result.Message = isValidPokemon.Message;
        result.Success = false;
      }
    } catch (Exception ex) {
      result.Message = "An error occurred while saving the pokemon";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
  }
  public async Task Edit(PokemonVM editPokemon) {
    ServiceResult result = new();
    try {
      var isValidPokemon = ValidatePokemon.IsValidPokemon(editPokemon);
      if (isValidPokemon.Success) {
        PokemonModel pokemonToUpdate = await _pokemonRepository.GetEntity(editPokemon.Id);
        if (pokemonToUpdate != null) {
          PokemonModel pokemon = editPokemon.ConvertToUpdate(pokemonToUpdate);
          await _pokemonRepository.Update(pokemon);
        } else {
          result.Message = "Pokemon not found";
          result.Success = false;
        }
      } else {
        result.Message = isValidPokemon.Message;
        result.Success = false;
      }

    } catch (Exception ex) {
      result.Message = "An error occurred while editing the pokemon";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
  }
  public async Task Delete(int deletePokemon) {
    ServiceResult result = new();
    try {
      PokemonModel pokemon = await _pokemonRepository.GetEntity(deletePokemon);
      if (pokemon != null) {
        await _pokemonRepository.Remove(pokemon);
      } else {
        result.Message = "Pokemon not found";
        result.Success = false;
      }
    } catch (Exception ex) {
      result.Message = "An error occurred while deleting the pokemon";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
  }
}
