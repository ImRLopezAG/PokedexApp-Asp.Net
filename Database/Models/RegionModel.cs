using Database.Core;

namespace Database.Models;

public class RegionModel : BaseModel {
  public ICollection<PokemonModel> Pokemons { get; set; }
}
