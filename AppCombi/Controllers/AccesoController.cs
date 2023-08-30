using AppCombi.Models;
using Microsoft.AspNetCore.Mvc;
using AppCombi.Data;

namespace AppCombi.Controllers
{
    public class AccesoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Index(Usuario _usuario)
        {
            ViajeContext _da_usuario = new ViajeContext();

            var usuario = _da_usuario.ValidarUsuario(_usuario.Correo, _usuario.Clave);

            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(usuario);
            }

        }
    }
}
