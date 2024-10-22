using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;

namespace BuscaEmprego.Controllers
{
    public class EmpresaController : DefaultController
    {
        private db_BuscaEmpregoContext _context;
        public RepositoryEmpresa _RepositoryEmpresa;

        public EmpresaController(db_BuscaEmpregoContext db)
        {
            _context = db;
            _RepositoryEmpresa = new RepositoryEmpresa(db);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _RepositoryEmpresa.SelecionarTodosAsync());
        }
        public async Task<IActionResult> Create(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                await _RepositoryEmpresa.IncluirAsync(empresa);
                ViewData["Mensagem"] = "Dados salvos com sucesso.";
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao salvar os dados.";
            }
            return View(empresa);
        }
    }
}
