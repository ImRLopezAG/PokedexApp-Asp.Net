using Application.Contracts;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex.Web.Controllers {
  public class RegionController : Controller {

    private readonly IRegionService _regionService;

    public RegionController(IRegionService regionService) {
      _regionService = regionService;
    }
    public async Task<IActionResult> Index() {
      return View(await _regionService.GetAll().ContinueWith(x => x.Result.Data));
    }

    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(RegionVM region) {
      try {
        if(!ModelState.IsValid)
          return View("Create");
        await _regionService.Save(region);
        return RedirectToAction(nameof(Index));
      } catch {
        return View(Create());
      }
    }
    public async Task<IActionResult> Edit(int id) {
      var region = await _regionService.GetById(id).ContinueWith(x => x.Result.Data);
      return View(region);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, RegionVM model) {
      try {
        await _regionService.Edit(model);
        return RedirectToAction(nameof(Index));
      } catch {
        return View(Edit(id));
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id) {
      try {
        await _regionService.Delete(id);
        return RedirectToAction(nameof(Index));
      } catch {
        return View(Index());
      }
    }
  }
}
