using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;

namespace BuscaEmprego.Controllers
{
    [Authorize(Roles = "admin, empresa")]
    public class EmpresaController : Controller
    {
        private readonly db_BuscaEmpregoContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RepositoryEmpresa _repositoryEmpresa;

        public EmpresaController(db_BuscaEmpregoContext db, UserManager<IdentityUser> userManager)
        {
            _context = db;
            _userManager = userManager;
            _repositoryEmpresa = new RepositoryEmpresa(db);
        }

        [Authorize(Roles = "admin, empresa")]
        public async Task<IActionResult> Index()
        {
            return View(await _repositoryEmpresa.SelecionarTodosAsync());
        }

        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);
            var empresa = new Empresa { UserId = userId };
            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                await _repositoryEmpresa.IncluirAsync(empresa);
                ViewData["Mensagem"] = "Dados salvos com sucesso.";
                return RedirectToAction("Details", new { cnpj = empresa.CNPJ });
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao salvar os dados.";
            }

            return View(empresa);
        }


        public async Task<IActionResult> Edit(string cnpj)
        {
            var empresa = await _repositoryEmpresa.SelecionarChaveAsync(cnpj);
            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Empresa empresa)
        {
            if (ModelState.IsValid)
            {
                await _repositoryEmpresa.AlterarAsync(empresa);
                ViewData["Mensagem"] = "Dados alterados com sucesso.";
                return RedirectToAction("Details");
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao alterar os dados.";
                return View(empresa);
            }
        }

        public async Task<IActionResult> Details(string cnpj)
        {
            var userId = _userManager.GetUserId(User);
            var empresa = await _repositoryEmpresa.SelecionarUserIdAsync(userId);

            if (empresa == null)
            {
                return RedirectToAction("Create");
            }

            return View(empresa);
        }

        public async Task<IActionResult> Delete(string cnpj)
        {
            var empresa = await _repositoryEmpresa.SelecionarChaveAsync(cnpj);
            return View(empresa);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Empresa empresa)
        {
            await _repositoryEmpresa.ExcluirAsync(empresa);
            return RedirectToAction("Index");
        }
    }
}
