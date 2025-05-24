using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using testTEA.Models;
using static System.Net.Mime.MediaTypeNames;

namespace testTEA.Controllers
{
    public class PreguntasController : Controller
    {
        private readonly testContext _context;

        public PreguntasController(testContext context)
        {
            _context = context;
        }

        // GET: Preguntas
        public async Task<IActionResult> Index(int id)
        {
            
            ViewBag.TestId = id;

            var preguntas = await _context.preguntas
                .Where(p => p.id_test == id)
            .ToListAsync();

            
            return View(preguntas);
        }
        public async Task<IActionResult> IndexPreguntas(int id)
        {


            var preguntas = await _context.preguntas
                .Where(p => p.id_test == id)
            .ToListAsync();

            
            return View(preguntas);
        }


     



        // GET: Preguntas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preguntas = await _context.preguntas
                .FirstOrDefaultAsync(m => m.id_pregunta == id);
            if (preguntas == null)
            {
                return NotFound();
            }

            return View(preguntas);
        }

        // GET: Preguntas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Preguntas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_pregunta,numero_pregunta,texto")] preguntas preguntas, int id)
        {
            int? ultimoTestId = HttpContext.Session.GetInt32("TestId");

            preguntas.id_test = ultimoTestId ?? id;

            if (ModelState.IsValid)
            {
                _context.Add(preguntas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index), new { id = preguntas.id_test });
            }
            ViewBag.TestId = preguntas.id_test;
            return View(preguntas);
        }
        

        // GET: Preguntas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preguntas = await _context.preguntas.FindAsync(id);
            if (preguntas == null)
            {
                return NotFound();
            }
            return View(preguntas);
        }

        // POST: Preguntas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_pregunta,id_test,numero_pregunta,texto")] preguntas preguntas)
        {
            if (id != preguntas.id_pregunta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(preguntas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!preguntasExists(preguntas.id_pregunta))
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
            return View(preguntas);
        }

        // GET: Preguntas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var preguntas = await _context.preguntas
                .FirstOrDefaultAsync(m => m.id_pregunta == id);
            if (preguntas == null)
            {
                return NotFound();
            }

            return View(preguntas);
        }

        // POST: Preguntas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var preguntas = await _context.preguntas.FindAsync(id);
            if (preguntas != null)
            {
                _context.preguntas.Remove(preguntas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool preguntasExists(int id)
        {
            return _context.preguntas.Any(e => e.id_pregunta == id);
        }
    }
}
