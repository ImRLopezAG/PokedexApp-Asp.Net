
using Application.Core;
using Application.ViewModels;

namespace Application.Validations;

public class ValidateRegion {
  public static ServiceResult IsValidRegion(RegionVM region) {
    ServiceResult result = new();
    if (string.IsNullOrEmpty(region.Name)) {
      result.Success = false;
      result.Message = "Name is required";
    }
    return result;
  }
}
