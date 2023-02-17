
namespace Application.ViewModels;

public class FilterVM {
  public int RegionId { get; set; }
  public int TypeId { get; set; }

  public string Name { get; set; }

  public List<TypeVM> Types { get; set; }
  public List<RegionVM> Regions { get; set; }

  public List<PokemonVM> Pokemons { get; set; }

}
