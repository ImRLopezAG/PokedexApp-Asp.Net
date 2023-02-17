using Database.Core;

namespace Database.Models;

public class TypeModel : BaseModel {
  public ICollection<PokemonModel> PrimaryType { get; set; }
  public ICollection<PokemonModel> SecondaryType { get; set; }
}
