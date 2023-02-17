using System.ComponentModel.DataAnnotations;

namespace Application.Core;

public class BaseVM {
  public int Id { get; set; }
  [Required(ErrorMessage = "You need to put the name")]
  public string Name { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
