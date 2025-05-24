using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using testTEA.Models;
using static System.Net.Mime.MediaTypeNames;

namespace testTEA.Controllers
{
    public class PadreController : Controller
    {
        private readonly testContext _context;
        public PadreController(testContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var id_test = from t in _context.tests
                          where t.nombre == "M-CHART"
                          select t.id_test;

            HttpContext.Session.SetInt32("IdTest", id_test.FirstOrDefault());

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCuestionario([Bind("id_cuestionario,id_paciente,id_test,fecha_realizacion,etapa,total_puntaje,resultado")] cuestionarios _cuestionario)
        {
            
            var id_paciente = from p in _context.pacientes
                              where p.id_usuario == HttpContext.Session.GetInt32("id_usuario")
                              select p.id_paciente;

            var id_test = _context.tests
                .Where(t => t.nombre == "M-CHART")
                .Select(t => t.id_test)
                .FirstOrDefault();

            _cuestionario.id_paciente = id_paciente.FirstOrDefault();
            _cuestionario.id_test = (int)id_test;

            if (ModelState.IsValid)
            {

                _context.Add(_cuestionario);
                await _context.SaveChangesAsync();
                return RedirectToAction("IndexPreguntas" , "Preguntas");

            }
            return View(_cuestionario);
        }
    }
}
