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
    public class CuestionariosController : Controller
    {
        private readonly testContext _context;

        public CuestionariosController(testContext context)
        {
            _context = context;
        }

        // GET: Cuestionarios
        public async Task<IActionResult> Index()
        {
            return View(await _context.cuestionarios.ToListAsync());
        }

        // GET: Cuestionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuestionarios = await _context.cuestionarios
                .FirstOrDefaultAsync(m => m.id_cuestionario == id);
            if (cuestionarios == null)
            {
                return NotFound();
            }

            return View(cuestionarios);
        }

        // GET: Cuestionarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CuestionariosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_cuestionario,id_paciente,id_test,fecha_realizacion,etapa,total_puntaje,resultado")] cuestionarios _cuestionario)
        {
            int? ultimoIdUsuario = HttpContext.Session.GetInt32("id_usuario");
            int? ultimoTestId = HttpContext.Session.GetInt32("TestId");

            _cuestionario.id_paciente = (int)ultimoIdUsuario;
            _cuestionario.id_test = (int)ultimoTestId;

            if (ModelState.IsValid)
            {

                _context.Add(_cuestionario);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexPreguntas");
            }
            return View(_cuestionario);
        }

        // GET: Cuestionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuestionarios = await _context.cuestionarios.FindAsync(id);
            if (cuestionarios == null)
            {
                return NotFound();
            }
            return View(cuestionarios);
        }

        // POST: Cuestionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_cuestionario,id_paciente,id_test,fecha_realizacion,etapa,total_puntaje,resultado")] cuestionarios cuestionarios)
        {
            if (id != cuestionarios.id_cuestionario)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuestionarios);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!cuestionariosExists(cuestionarios.id_cuestionario))
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
            return View(cuestionarios);
        }

        // GET: Cuestionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuestionarios = await _context.cuestionarios
                .FirstOrDefaultAsync(m => m.id_cuestionario == id);
            if (cuestionarios == null)
            {
                return NotFound();
            }

            return View(cuestionarios);
        }

        // POST: Cuestionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuestionarios = await _context.cuestionarios.FindAsync(id);
            if (cuestionarios != null)
            {
                _context.cuestionarios.Remove(cuestionarios);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool cuestionariosExists(int id)
        {
            return _context.cuestionarios.Any(e => e.id_cuestionario == id);
        }
    }
}
