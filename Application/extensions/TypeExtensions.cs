using Application.ViewModels;
using Database.Models;

namespace Application.extensions;

public static class TypeExtensions {
  public static TypeModel ConvertToSave(this TypeVM saveType) => new TypeModel {
    Name = saveType.Name,
    CreatedAt = DateTime.Now
  };

  public static TypeModel ConvertToUpdate(this TypeVM updateType, TypeModel type) {
    type.Name = updateType.Name;
    type.UpdatedAt = DateTime.Now;
    return type;
  }

  public static TypeVM ConvertToTypeVM(this TypeModel type) => new TypeVM {
    Id = type.Id,
    Name = type.Name,
    CreatedAt = type.CreatedAt,
    UpdatedAt = type.UpdatedAt
  };
}
