using Application.Contracts;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Web.Models;
using System.Diagnostics;

namespace Pokedex.Web.Controllers {
  public class HomeController : Controller {
    private readonly ILogger<HomeController> _logger;
    private readonly IPokemonService _pokemonService;
    private readonly IRegionService _regionService;
    private readonly ITypeService _typeService;

    public HomeController(ILogger<HomeController> logger, IPokemonService pokemonService, IRegionService regionService, ITypeService typeService) {
      _logger = logger;
      _pokemonService = pokemonService;
      _regionService = regionService;
      _typeService = typeService;
    }

    public async Task<IActionResult> Index() {
      return View("Index", new FilterVM() {
        Regions = ( List<RegionVM> )await _regionService.GetAll().ContinueWith(x => x.Result.Data),
        Types = ( List<TypeVM> )await _typeService.GetAll().ContinueWith(x => x.Result.Data),
        Pokemons = ( List<PokemonVM> )await _pokemonService.GetAll().ContinueWith(x => x.Result.Data)
      });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FilterByRegion(int regionId) {
      try {
        return View("Index", new FilterVM() {
          Regions = ( List<RegionVM> )await _regionService.GetAll().ContinueWith(x => x.Result.Data),
          Types = ( List<TypeVM> )await _typeService.GetAll().ContinueWith(x => x.Result.Data),
          Pokemons = ( List<PokemonVM> )await _pokemonService.GetByRegion(new FilterVM { RegionId = regionId }).ContinueWith(x => x.Result.Data)
        });
      } catch {
        return View("Index", new FilterVM() {
          Regions = ( List<RegionVM> )await _regionService.GetAll().ContinueWith(x => x.Result.Data),
          Types = ( List<TypeVM> )await _typeService.GetAll().ContinueWith(x => x.Result.Data),
          Pokemons = ( List<PokemonVM> )await _pokemonService.GetAll().ContinueWith(x => x.Result.Data)
        });
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FilterByType(int typeId) {
      try {
        return View("Index", new FilterVM() {
          Regions = ( List<RegionVM> )await _regionService.GetAll().ContinueWith(x => x.Result.Data),
          Types = ( List<TypeVM> )await _typeService.GetAll().ContinueWith(x => x.Result.Data),
          Pokemons = ( List<PokemonVM> )await _pokemonService.GetByType(new FilterVM { TypeId = typeId }).ContinueWith(x => x.Result.Data)
        });
      } catch {
        return View("Index", new FilterVM() {
          Regions = ( List<RegionVM> )await _regionService.GetAll().ContinueWith(x => x.Result.Data),
          Types = ( List<TypeVM> )await _typeService.GetAll().ContinueWith(x => x.Result.Data),
          Pokemons = ( List<PokemonVM> )await _pokemonService.GetAll().ContinueWith(x => x.Result.Data)
        });
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> FilterByName(string name) {
      try {
        return View("Index", new FilterVM() {
          Regions = ( List<RegionVM> )await _regionService.GetAll().ContinueWith(x => x.Result.Data),
          Types = ( List<TypeVM> )await _typeService.GetAll().ContinueWith(x => x.Result.Data),
          Pokemons = ( List<PokemonVM> )await _pokemonService.GetByName(new FilterVM { Name = name }).ContinueWith(x => x.Result.Data)
        });
      } catch {
        return View("Index", new FilterVM() {
          Regions = ( List<RegionVM> )await _regionService.GetAll().ContinueWith(x => x.Result.Data),
          Types = ( List<TypeVM> )await _typeService.GetAll().ContinueWith(x => x.Result.Data),
          Pokemons = ( List<PokemonVM> )await _pokemonService.GetAll().ContinueWith(x => x.Result.Data)

        });
      }
    }
    public IActionResult Error() {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}