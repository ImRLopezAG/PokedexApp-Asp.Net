using Application.Core;
using Application.ViewModels;

namespace Application.Contracts;

public interface ITypeService : IBaseService {
  Task Save(TypeVM saveType);
  Task Edit(TypeVM editType);
  Task Delete(int deleteType);
}
