using Application.Core;
using Application.ViewModels;

namespace Application.Contracts;

public interface IRegionService : IBaseService {
  Task Save(RegionVM saveRegion);
  Task Edit(RegionVM editRegion);
  Task Delete(int deleteRegion);
}
