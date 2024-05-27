using Microsoft.AspNetCore.Mvc;

namespace frontendnet;

public class AArchivosController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}