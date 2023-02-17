using Application.Contracts;
using Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Pokedex.Web.Controllers {
  public class TypeController : Controller {

    private readonly ITypeService _typeService;

    public TypeController(ITypeService typeService) {
      _typeService = typeService;
    }

    public async Task<IActionResult> Index() {
      return View(await _typeService.GetAll().ContinueWith(x => x.Result.Data));
    }
    public IActionResult Create() {
      return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Create(TypeVM type) {
      try {
        if (!ModelState.IsValid)
          return View("Create");
        await _typeService.Save(type);
        return RedirectToAction(nameof(Index));
      } catch {
        return View(Create());
      }
    }

    public async Task<IActionResult> Edit(int id) {
      var type = await _typeService.GetById(id).ContinueWith(x => x.Result.Data);
      return View(type);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Edit(int id, TypeVM model) {
      try {
        await _typeService.Edit(model);
        return RedirectToAction(nameof(Index));
      } catch {
        return View(Edit(id));
      }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Delete(int id) {
      try {
        await _typeService.Delete(id);
        return RedirectToAction(nameof(Index));
      } catch {
        return View();
      }
    }
  }
}
