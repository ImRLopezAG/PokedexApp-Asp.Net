using Application.Contracts;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex.Web.Controllers {
  public class PokemonController : Controller {

    private readonly IPokemonService _pokemonService;
    private readonly IRegionService _regionService;
    private readonly ITypeService _typeService;

    public PokemonController(IPokemonService pokemonService, IRegionService regionService, ITypeService typeService) {
      _pokemonService = pokemonService;
      _regionService = regionService;
      _typeService = typeService;
    }
    public async Task<IActionResult> Index() {
      return View(await _pokemonService.GetAll().ContinueWith(x => x.Result.Data));
    }

    public async Task<IActionResult> Create() {
      return View("Create", new PokemonVM() {
        Regions = ( List<RegionVM> )await _regionService.GetAll().ContinueWith(x => x.Result.Data),
        Types = ( List<TypeVM> )await _typeService.GetAll().ContinueWith(x => x.Result.Data)
      }
      );
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(PokemonVM pokemon) {
      try {
        if (!ModelState.IsValid)
          return View("Create", new PokemonVM() {
            Regions = ( List<RegionVM> )await _regionService.GetAll().ContinueWith(x => x.Result.Data),
            Types = ( List<TypeVM> )await _typeService.GetAll().ContinueWith(x => x.Result.Data)
          });
        await _pokemonService.Save(pokemon);
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }
    public async Task<IActionResult> Edit(int id) {
      return View("Edit", await _pokemonService.GetById(id).ContinueWith(x => x.Result.Data));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, PokemonVM pokemon) {
      try {
        await _pokemonService.Edit(pokemon);
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id) {
      try {
        await _pokemonService.Delete(id);
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }
  }
}
