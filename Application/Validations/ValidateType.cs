using Application.Core;
using Application.ViewModels;

namespace Application.Validations;

public class ValidateType {
  public static ServiceResult IsValidType(TypeVM type) {
    ServiceResult result = new();
    if (string.IsNullOrEmpty(type.Name)) {
      result.Success = false;
      result.Message = "Name is required";
    }
    return result;
  }
}
