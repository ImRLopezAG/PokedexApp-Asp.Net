using Database.Core;

namespace Database.Models;

public class PokemonModel : BaseModel {
  public string Image { get; set; }
  // Foreign Keys
  public int RegionId { get; set; }
  public int PrimaryTypeId { get; set; }
  public int? SecondaryTypeId { get; set; }
  // Navigation Properties
  public RegionModel Regions { get; set; }
  public TypeModel PrimaryType { get; set; }
  public TypeModel? SecondaryType { get; set; }
}
