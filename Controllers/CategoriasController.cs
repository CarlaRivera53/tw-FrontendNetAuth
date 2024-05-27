using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace frontendnet;
[Authorize(Roles =  "administrador")]
public class  CategoriasController(CategoriasClientService categorias) : Controller
{
   public async Task<IActionResult> Index()
   {
    List<Categoria>? lista = [];
    try 
    {
     lista = await categorias.GetAsync();
    }
    catch (HttpRequestException ex)
    {
        if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        return RedirectToAction("Salir","Auth");
    }
    return View(lista);
   } 
   public async Task<IActionResult> Detalle(int id)
   {
    Categoria? item = null;
    try
    {
        item = await categorias.GetAsync(id);
        if (item == null) return NotFound();
    }
    catch (HttpRequestException ex)
    {
        if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        return RedirectToAction("Salir", "Auth");

    }
    return View(item);
   }
  public IActionResult Crear()
  {
    return View();
  }
  [HttpPost]
  public async Task<IActionResult> CrearAsync(Categoria itemToCreate)
  {
    if(ModelState.IsValid)
    {
        try{
            await categorias.PostAsync(itemToCreate);
            return RedirectToAction(nameof(Index));
        }
        catch(HttpRequestException ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            return RedirectToAction("Salir" , "Auth");
        }
    }
    ModelState.AddModelError("Nombre", "No ha sido posible realizar la accion. intenteo nuevamente");
    return View(itemToCreate);
  }
  public async Task<IActionResult> EditarAsynx(int id)
  {
    Categoria? itemToEdit = null;
    try
    {
     itemToEdit = await categorias.GetAsync(id);
    if (itemToEdit == null) return NotFound();
    }
    catch (HttpRequestException ex)
    {
    if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
    return RedirectToAction("Salir", "Auth");

  }
  return View(itemToEdit);
  }
  [HttpPost]
  public async Task<IActionResult> EditarAsync(int id, Categoria itemToEdit)
{
  if(id != itemToEdit.CategoriaId) return NotFound();
  {
    try {
      await categorias.PutAsync(itemToEdit);
      return RedirectToAction(nameof(Index));
    }
    catch (HttpRequestException ex)
    {
      if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
      return RedirectToAction("Salir", "Auth");   
       }
  }
  ModelState.AddModelError("Nombre", "No ha sido posible la accion. Intentelo nuevamente");
return View(itemToEdit); 
} 
public async Task<IActionResult> Eliminar(int id, bool? showError = false)
{
  Categoria? itemToDelete = null;
   try{
    itemToDelete = await categorias.GetAsync(id);
    if (itemToDelete == null) return NotFound();

    if (showError.GetValueOrDefault())
    ViewData["ErrorMessage"] = "No ha sido posible realizar la accion. Intentelo nuevamente";
   }
   catch (HttpRequestException ex)
   {
    if(ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
    return RedirectToAction("Salir", "Auth");
   }
   return View(itemToDelete);
}
[HttpPost]
public async Task<IActionResult> Eliminar(int id)
{
  if (ModelState.IsValid)
  {
    try
    {
      await categorias.DeleteAsync(id);
      return RedirectToAction(nameof(Index));

    }
    catch (HttpRequestException ex)
    {
      if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
      return RedirectToAction("Salir", "Auth");
    }
  }
  return RedirectToAction(nameof(Eliminar), new{id, showwError = true});
}

}