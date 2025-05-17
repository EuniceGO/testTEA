using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testTEA.Models;

namespace testTEA.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly testContext _testContext;

        public LoginController(testContext context)
        {
            _testContext = context;
        }

        // GET: Registro
        public IActionResult Registro()
        {
            return View();
        }

        // POST: Registro
        [HttpPost]
        public IActionResult Registro(Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                // Verificar si ya existe un usuario con ese correo
                var existe = _testContext.usuarios.FirstOrDefault(u => u.correo == usuario.correo);
                if (existe != null)
                {
                    ViewBag.Error = "El correo ya está registrado.";
                    return View(usuario);
                }

                _testContext.usuarios.Add(usuario);
                _testContext.SaveChanges();

                // Guardamos la sesión con los datos del usuario
                HttpContext.Session.SetInt32("id_usuario", usuario.id_usuario);
                HttpContext.Session.SetString("nombre", usuario.nombre);
                HttpContext.Session.SetString("rol", usuario.rol);

                return RedirectToAction("Login", "Login");
            }

            return View(usuario);
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public IActionResult Login(string correo, string clave)
        {
            var user = _testContext.usuarios
                .FirstOrDefault(u => u.correo == correo && u.contrasena == clave);

            if (user != null)
            {
                HttpContext.Session.SetInt32("id_usuario", user.id_usuario);
                HttpContext.Session.SetString("nombre", user.nombre);
                HttpContext.Session.SetString("rol", user.rol);

                if (user.rol == "Padre")
                {
                    return RedirectToAction("Index", "Padre");
                }
                else if (user.rol == "Profesional")
                {
                    return RedirectToAction("Index", "Profesional");
                }
            }

            ViewBag.Error = "correo o clave incorrectos.";
            return View();
        }


        // Cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
