using Application.Core;

namespace Application.Validations;

public class ValidatePokemon {
  public static ServiceResult IsValidPokemon(PokemonVM pokemon) {
    ServiceResult result = new();
    if (string.IsNullOrEmpty(pokemon.Name)) {
      result.Success = false;
      result.Message = "Name is required";
      return result;
    }
    if (string.IsNullOrEmpty(pokemon.PrimaryTypeId.ToString())) {
      result.Success = false;
      result.Message = "Type is required";
      return result;
    }
    if (string.IsNullOrEmpty(pokemon.Image)) {
      result.Success = false;
      result.Message = "Image is required";
      return result;
    }
    return result;
  }
}
