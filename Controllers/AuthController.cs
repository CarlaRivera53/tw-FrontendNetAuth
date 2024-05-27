using System.Security.Claims;
using frontendnet.Models;
using frontendnet.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace frontendnet;

public class AuthController(AuthClientService auth) : Controller
{
 [AllowAnonymous]
 public IActionResult Index()
 {
    return View();
 }
 [HttpPost]
 [AllowAnonymous]
 [ValidateAntiForgeryToken]
 public async Task<IActionResult> IndexAsync(Login Model)
 {
    if (ModelState.IsValid)
    {
        try{
            //esta funcion verifica en backnend que el correo y contrasela sean validos
            var token = await auth.ObtenTokenAsync(Model.Email, Model.Password);
            var claims = new List<Claim>
            {
                //todo esto se guarda en la cookie
                new(ClaimTypes.Name, token.Email),
                new(ClaimTypes.GivenName, token.Nombre),
                new("jwt", token.Rol),
                };
                auth.IniciaSesionAsync(claims);
                //usuario valido, lo encia a la lista de peliculas
                return RedirectToAction("Index", "Peliculas");
            }
            catch (Exception)
            {
                ModelState.AddModelError("Email", "credenciales no validas. Intentelo nuevamente.");

            }
        }
        return View(Model);
    }
    [Authorize(Roles = "Administradro, usuario")]
    public async Task<IActionResult> SalirAsync()
    {
      //cierra la sesion 
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      //sino, se redirege a la pagina inicial
      return RedirectToAction("Index", "Auth");  
    }

}

