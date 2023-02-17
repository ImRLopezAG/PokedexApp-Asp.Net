using Application.Core;
using Application.ViewModels;
using System.ComponentModel.DataAnnotations;

public class PokemonVM : BaseVM {
  [Required(ErrorMessage = "You need to set the image of pokemon")]

  public string Image { get; set; }

  public string Region { get; set; }
  public string PrimaryType { get; set; }

  public string SecondaryType { get; set; }

  // Foreign Keys
  [Required(ErrorMessage = "You need to set the region of your pokemon")]

  public int RegionId { get; set; }
  [Required(ErrorMessage = "You need to set the primary type of your pokemon")]

  public int PrimaryTypeId { get; set; }

  public int? SecondaryTypeId { get; set; }

  // Navigation Properties
  public List<TypeVM> Types { get; set; }

  public List<RegionVM> Regions { get; set; }

}
