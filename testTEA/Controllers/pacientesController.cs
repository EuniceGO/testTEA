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
    public class pacientesController : Controller
    {
        private readonly testContext _context;

        public pacientesController(testContext context)
        {
            _context = context;
        }

        // GET: pacientes
        public async Task<IActionResult> Index()
        {
            return View(await _context.pacientes.ToListAsync());
        }

        // GET: pacientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacientes = await _context.pacientes
                .FirstOrDefaultAsync(m => m.id_paciente == id);
            if (pacientes == null)
            {
                return NotFound();
            }

            return View(pacientes);
        }

        // GET: pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_paciente,id_usuario,nombre,edad_meses,genero,fecha_nacimiento")] pacientes pacientes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacientes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pacientes);
        }

        // GET: pacientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacientes = await _context.pacientes.FindAsync(id);
            if (pacientes == null)
            {
                return NotFound();
            }
            return View(pacientes);
        }

        // POST: pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_paciente,id_usuario,nombre,edad_meses,genero,fecha_nacimiento")] pacientes pacientes)
        {
            if (id != pacientes.id_paciente)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!pacientesExists(pacientes.id_paciente))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pacientes);
        }

        // GET: pacientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacientes = await _context.pacientes
                .FirstOrDefaultAsync(m => m.id_paciente == id);
            if (pacientes == null)
            {
                return NotFound();
            }

            return View(pacientes);
        }

        // POST: pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacientes = await _context.pacientes.FindAsync(id);
            if (pacientes != null)
            {
                _context.pacientes.Remove(pacientes);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool pacientesExists(int id)
        {
            return _context.pacientes.Any(e => e.id_paciente == id);
        }
    }
}
