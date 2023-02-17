using Application.Contracts;
using Application.Core;
using Application.extensions;
using Application.Validations;
using Application.ViewModels;
using Database.Interfaces;
using Database.Models;

namespace Application.Services;

public class RegionService : IRegionService {
  private readonly IRegionRepository _regionRepository;
  private readonly ILoggerService<RegionService> _loggerService;

  public RegionService(IRegionRepository regionRepository, ILoggerService<RegionService> loggerService) {
    _regionRepository = regionRepository;
    _loggerService = loggerService;
  }

  public async Task<ServiceResult> GetAll() {
    ServiceResult result = new();
    try {
      var query = from region in await _regionRepository.GetAll()
                  select region.ConvertToRegionVM();
      result.Data = query.ToList();
    } catch (Exception ex) {
      result.Message = "An error occurred while getting the regions";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
    return result;
  }

  public async Task<ServiceResult> GetById(int id) {
    ServiceResult result = new();
    try {
      RegionModel region = await _regionRepository.GetEntity(id);
      if (region != null) {
        result.Data = region.ConvertToRegionVM();
      } else {
        result.Message = "No region found";
        result.Success = false;
      }
    } catch (Exception ex) {
      result.Message = "An error occurred while getting the region";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
    return result;
  }
  public async Task Save(RegionVM saveRegion) {
    ServiceResult result = new();
    try {
      var validation = ValidateRegion.IsValidRegion(saveRegion);
      if (validation.Success) {
        RegionModel region = saveRegion.ConvertToSave();
        await _regionRepository.Save(region);

      } else {
        result.Message = "An error occurred while saving the region";
        result.Success = false;
      }
    } catch (Exception ex) {
      result.Message = "An error occurred while saving the region";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
  }

  public async Task Edit(RegionVM editRegion) {
    ServiceResult result = new();
    try {
      var validation = ValidateRegion.IsValidRegion(editRegion);
      if (validation.Success) {
        RegionModel regionToUpdate = await _regionRepository.GetEntity(editRegion.Id);
        if (regionToUpdate != null) {
          RegionModel region = editRegion.ConvertToUpdate(regionToUpdate);
          await _regionRepository.Update(region);
        } else {
          result.Message = "A region with that name already exists";
          result.Success = false;
        }
      } else {
        result.Message = "An error occurred while editing the region";
        result.Success = false;
      }
    } catch (Exception ex) {
      result.Message = "An error occurred while editing the region";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
  }
  public async Task Delete(int deleteRegion) {
    ServiceResult result = new();
    try {
      RegionModel region = await _regionRepository.GetEntity(deleteRegion);
      if (region != null) {
        await _regionRepository.Remove(region);
      } else {
        result.Message = "No region found";
        result.Success = false;
      }
    } catch (Exception ex) {
      result.Message = "An error occurred while deleting the region";
      result.Success = false;
      _loggerService.LogError(ex.Message);
    }
  }
}
