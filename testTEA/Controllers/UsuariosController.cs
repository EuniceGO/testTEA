using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testTEA.Models;

namespace testTEA.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly testContext _context;

        public UsuariosController(testContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        // GET: Usuarios
        public async Task<IActionResult> Index(string nombre)
        {
            var usuarios = from u in _context.usuarios select u;

            if (!string.IsNullOrEmpty(nombre))
            {
                usuarios = usuarios.Where(u => u.nombre.Contains(nombre));
            }

            return View(await usuarios.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.id_usuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_usuario,nombre,correo,contrasena,rol,numeroSello,Estado,telefono")] Usuario usuario)
        {

            
           

            if (ModelState.IsValid)
            {
                // Hashear la contraseña
                usuario.contrasena = SeguridadHelper.HashPassword(usuario.contrasena);
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_usuario,nombre,correo,contrasena,rol,numeroSello,Estado,telefono")] Usuario usuario)
        {
            if (id != usuario.id_usuario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Obtener el usuario original desde la base de datos
                    var usuarioExistente = await _context.usuarios.FindAsync(id);
                    if (usuarioExistente == null)
                    {
                        return NotFound();
                    }

                    // Actualizar campos normales
                    usuarioExistente.nombre = usuario.nombre;
                    usuarioExistente.correo = usuario.correo;
                    usuarioExistente.rol = usuario.rol;
                    usuarioExistente.numeroSello = usuario.numeroSello;
                    usuarioExistente.Estado = usuario.Estado;
                    usuarioExistente.telefono = usuario.telefono;

                    // Solo actualizar contraseña si se ingresó una nueva
                    if (!string.IsNullOrWhiteSpace(usuario.contrasena))
                    {
                        usuarioExistente.contrasena = SeguridadHelper.HashPassword(usuario.contrasena);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.id_usuario))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(usuario);
        }


        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.usuarios
                .FirstOrDefaultAsync(m => m.id_usuario == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var usuario = await _context.usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.usuarios.Remove(usuario);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return _context.usuarios.Any(e => e.id_usuario == id);
        }
    }
}
