using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testTEA.Models;
using System.Security.Cryptography;

namespace testTEA.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly testContext _testContext;
        private readonly EmailService _emailService;

        public LoginController(testContext context, EmailService emailService)
        {
            _testContext = context;
            _emailService = emailService;
            _emailService = emailService;
        }

        // GET: Registro
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registro(Usuario usuario, string confirmarContrasena)
        {
           
            if (ModelState.IsValid)
            {
                if (usuario.contrasena != confirmarContrasena)
                {
                    ViewBag.Error = "Las contraseñas no coinciden.";
                    return View(usuario);
                }




                var existe = _testContext.usuarios.FirstOrDefault(u => u.correo == usuario.correo);
                if (existe != null)
                {
                    ViewBag.Error = "El correo ya está registrado.";
                    return View(usuario);
                }

                usuario.Estado ??= "Pendiente";
                usuario.numeroSello ??= "";

                // Hashear la contraseña
                usuario.contrasena = SeguridadHelper.HashPassword(usuario.contrasena);

                try
                {
                    _testContext.usuarios.Add(usuario);
                    _testContext.SaveChanges();

                    // Recargar el usuario para obtener el ID autogenerado
                    _testContext.Entry(usuario).Reload();

                    HttpContext.Session.SetInt32("id_usuario", usuario.id_usuario);
                    HttpContext.Session.SetString("nombre", usuario.nombre);
                    HttpContext.Session.SetString("rol", usuario.rol);
                    HttpContext.Session.SetString("numeroSello", usuario.numeroSello);
                    HttpContext.Session.SetString("telefono", usuario.telefono);

                    _emailService.EnviarCorreo(
                              "Nueva solicitud de acceso",
                              $"Se ha registrado un nuevo usuario con correo: {usuario.correo} y rol: {usuario.rol}.\n" +
                              $"Estado: {usuario.Estado}\nNúmero de Teléfono: {usuario.telefono}"
                                                                                                 );

                    return RedirectToAction("Login", "Login");


                   
                }
                catch (Exception ex)
                {
                    ViewBag.Error = "Ocurrió un error al registrar el usuario: " + ex.Message;
                    return View(usuario);
                }
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
                if (user.Estado != "Aprobado")
                {
                    ViewBag.Error = "Tu cuenta aún no ha sido aprobada por un administrador.";
                    return View();
                }


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
                }else if (user.rol == "Administrador")
                {
                    return RedirectToAction("Index_admin", "Admin");
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
