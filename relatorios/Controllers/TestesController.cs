
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using relatorios.Data;
using relatorios.Models;

namespace relatorios.Controllers
{
    public class TestesController : Controller
    {
        private readonly relatoriosContext _context;

        public TestesController(relatoriosContext context)
        {
            _context = context;
        }

        // GET: Testes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Teste.ToListAsync());
        }

        // GET: Testes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teste = await _context.Teste
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teste == null)
            {
                return NotFound();
            }

            return View(teste);
        }

        // GET: Testes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Testes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Idade,Apelido")] Teste teste)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teste);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(teste);
        }

        // GET: Testes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teste = await _context.Teste.FindAsync(id);
            if (teste == null)
            {
                return NotFound();
            }
            return View(teste);
        }

        // POST: Testes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Idade,Apelido")] Teste teste)
        {
            if (id != teste.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teste);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TesteExists(teste.Id))
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
            return View(teste);
        }

        // GET: Testes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teste = await _context.Teste
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teste == null)
            {
                return NotFound();
            }

            return View(teste);
        }

        // POST: Testes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teste = await _context.Teste.FindAsync(id);
            _context.Teste.Remove(teste);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TesteExists(int id)
        {
            return _context.Teste.Any(e => e.Id == id);
        }



    }
}
