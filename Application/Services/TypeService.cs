using Application.Contracts;
using Application.Core;
using Application.extensions;
using Application.Validations;
using Application.ViewModels;
using Database.Interfaces;
using Database.Models;

namespace Application.Services;

public class TypeService : ITypeService {
  private readonly ITypeRepository _typeRepository;
  private readonly IPokemonRepository _pokemonRepository;
  private readonly ILoggerService<TypeService> _loggerService;

  public TypeService(ITypeRepository typeRepository, ILoggerService<TypeService> loggerService, IPokemonRepository pokemonRepository) {
    _typeRepository = typeRepository;
    _loggerService = loggerService;
    _pokemonRepository = pokemonRepository;
  }

  public async Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var query = from type in await _typeRepository.GetAll()
                  select type.ConvertToTypeVM();
      result.Data = query.ToList();
    } catch (Exception ex) {
      result.Message = "An error occurred while getting the types";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
    return result;
  }

  public async Task<ServiceResult> GetById(int id) {
    ServiceResult result = new();
    try {
      TypeModel type = await _typeRepository.GetEntity(id);
      if (type != null) {
        result.Data = type.ConvertToTypeVM();
      } else {
        result.Message = "Type not found";
        result.Success = false;
      }
    } catch (Exception ex) {
      result.Message = "An error occurred while getting the type";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
    return result;
  }
  public async Task Save(TypeVM saveType) {
    ServiceResult result = new();
    try {
      var isValidType = ValidateType.IsValidType(saveType);
      if (isValidType.Success) {
        TypeModel type = saveType.ConvertToSave();
        await _typeRepository.Save(type);
      } else {
        result.Message = isValidType.Message;
        result.Success = false;
      }
    } catch (Exception ex) {
      result.Message = "An error occurred while saving the type";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
  }
  public async Task Edit(TypeVM editType) {
    ServiceResult result = new();
    try {
      var isValidType = ValidateType.IsValidType(editType);
      if (isValidType.Success) {
        TypeModel typeToUpdate = await _typeRepository.GetEntity(editType.Id);
        if (typeToUpdate != null) {
          TypeModel type = editType.ConvertToUpdate(typeToUpdate);
          await _typeRepository.Update(type);

        } else {
          result.Message = "Type not found";
          result.Success = false;
        }
      } else {
        result.Message = isValidType.Message;
        result.Success = false;
      }
    } catch (Exception ex) {
      result.Message = "An error occurred while editing the type";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
  }

  public async Task Delete(int deleteType) {
    ServiceResult result = new();
    try {
      TypeModel type = await _typeRepository.GetEntity(deleteType);
      if (type != null) {
        var pokemons = await _pokemonRepository.GetAll().ContinueWith(x => x.Result.Where(pk => pk.PrimaryTypeId == type.Id || pk.SecondaryTypeId == type.Id));
        foreach (var pokemon in pokemons) {
          if (pokemon.PrimaryTypeId == type.Id) {
            await _pokemonRepository.Remove(pokemon);
          } else {
            pokemon.SecondaryTypeId = null;
          }
        }
        await _typeRepository.Remove(type);
      } else {
        result.Message = "Type not found";
        result.Success = false;
      }
    } catch (Exception ex) {
      result.Message = "An error occurred while deleting the type";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
  }
}
