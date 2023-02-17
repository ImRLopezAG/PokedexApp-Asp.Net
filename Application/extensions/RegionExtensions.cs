using Application.ViewModels;
using Database.Models;

namespace Application.extensions;

public static class RegionExtensions {
  public static RegionModel ConvertToSave(this RegionVM saveRegion) => new RegionModel {
    Name = saveRegion.Name,
    CreatedAt = DateTime.Now
  };

  public static RegionModel ConvertToUpdate(this RegionVM updateRegion, RegionModel region) {
    region.Name = updateRegion.Name;
    region.UpdatedAt = DateTime.Now;
    return region;
  }

  public static RegionVM ConvertToRegionVM(this RegionModel region) => new RegionVM {
    Id = region.Id,
    Name = region.Name,
    CreatedAt = region.CreatedAt,
    UpdatedAt = region.UpdatedAt
  };
}
