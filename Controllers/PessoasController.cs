using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Controllers
{
    public class PessoasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PessoasController(ApplicationDbcontext context)
        {
            _context = context
        }

        public IActionResult Index()
        {
            return View(_context.Cars.ToList());
        }

        public IActtionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoa);
                _context.SaveChanges();
                return RedirectToAction(nameof("Index"));
            }
            return View(pessoa);    
        }

        public async Task<IActionResult>Edit (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pessoa = await _context.Pessoa.FindByIdAsync(id);
            if ( pessoa = null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit (inmt id[Bind("Id, Nome, Sobrenome, Idade, Profissao")] Pessoa pessoa)
        {
            if (id !=pessoa.Id)
            {
                return NotFound();
            }

            if (MoldelState.IsValid)
            {
                try{
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.Id))
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
            return View(pessoa);
        }


        public async Task<IActionResult> delete (int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoas
                .FirstOrDefaultAsync(m => m.Id == id);
                if ( pessoa == null)
                {
                    return NotFound();
                }
                return View(pessoa);
         }

         [HttpPost, ActionName("Delete")]
         [ValidateAntiForgeryToken]

         public async Task<IActionResult> DeleteCofnirmed(int id)
         {
            var pessoa = await _context.Pessoas.FindAsync(id);
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
         }

         private bool PessoaExists(int id)
         {
            return _context.Pessoas.Any(e => e.Id == id);
         }
     }
}        