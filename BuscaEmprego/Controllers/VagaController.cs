
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;

namespace BuscaEmprego.Controllers
{
    public class VagaController : Controller
    {
        private db_BuscaEmpregoContext _context;
        public RepositoryVaga _RepositoryVaga;

        public VagaController(db_BuscaEmpregoContext db)
        {
            _context = db;
            _RepositoryVaga = new RepositoryVaga(db);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _RepositoryVaga.SelecionarTodosAsync());
        }
    }
}
