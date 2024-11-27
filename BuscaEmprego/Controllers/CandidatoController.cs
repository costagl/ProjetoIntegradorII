using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Model.Models;
using Model.Repositories;

namespace BuscaEmprego.Controllers
{
    [Authorize(Roles = "admin, candidato")]
    public class CandidatoController : Controller
    {
        private readonly db_BuscaEmpregoContext _context;
        private readonly RepositoryCandidato _repositoryCandidato;
        private readonly UserManager<IdentityUser> _userManager;

        public CandidatoController(db_BuscaEmpregoContext db, UserManager<IdentityUser> userManager)
        {
            _context = db;
            _userManager = userManager;
            _repositoryCandidato = new RepositoryCandidato(db);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            return View(await _repositoryCandidato.SelecionarTodosAsync());
        }

        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);
            var candidato = new Candidato { UserId = userId };
            return View(candidato);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Candidato candidato)
        {
            if (ModelState.IsValid)
            {
                await _repositoryCandidato.IncluirAsync(candidato);
                ViewData["Mensagem"] = "Dados salvos com sucesso.";
                return RedirectToAction("Details", new { cpf = candidato.CPF });
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao salvar os dados.";
            }

            return View(candidato);
        }


        public async Task<IActionResult> Edit(string cpf)
        {
            var candidato = await _repositoryCandidato.SelecionarChaveAsync(cpf);
            
            return View(candidato);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Candidato candidato)
        {
            if (ModelState.IsValid)
            {
                await _repositoryCandidato.AlterarAsync(candidato);
                ViewData["Mensagem"] = "Dados alterados com sucesso.";
                return RedirectToAction("Details", new { cpf = candidato.CPF });
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao alterar os dados.";
                return View(candidato);
            }
        }

        public async Task<IActionResult> Details(string cpf)
        {
            var userId = _userManager.GetUserId(User);
            var candidato = await _repositoryCandidato.SelecionarUserIdAsync(userId);

            if (candidato == null)
            {
                return RedirectToAction("Create");
            }

            return View(candidato);
        }


        public async Task<IActionResult> Delete(string cpf)
        {
            var candidato = await _repositoryCandidato.SelecionarChaveAsync(cpf);
            return View(candidato);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Candidato candidato)
        {
            await _repositoryCandidato.ExcluirAsync(candidato);
            return RedirectToAction("Index");
        }
    }
}
