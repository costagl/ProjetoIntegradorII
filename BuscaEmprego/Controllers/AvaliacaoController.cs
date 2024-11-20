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
    public class AvaliacaoController : Controller
    {
        private readonly db_BuscaEmpregoContext _context;
        private readonly RepositoryAvaliacao _repositoryAvaliacao;
        private readonly UserManager<IdentityUser> _userManager;

        public AvaliacaoController(db_BuscaEmpregoContext db, UserManager<IdentityUser> userManager)
        {
            _context = db;
            _userManager = userManager;
            _repositoryAvaliacao = new RepositoryAvaliacao(db);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repositoryAvaliacao.SelecionarTodosAsync());
        }

        public async Task<IActionResult> IndexListaPorCPF(string cpf)
        {
            return View(await _repositoryAvaliacao.SelecionarTodosPorCPFAsync(cpf));
        }

        public IActionResult Create(string cpf)
        {
            var avaliacao = new Avaliacao { CPF_Candidato = cpf };
            avaliacao.CPF_Candidato = "178.734.267-00";
            return View(avaliacao);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                await _repositoryAvaliacao.IncluirAsync(avaliacao);
                ViewData["Mensagem"] = "Dados salvos com sucesso.";
                return RedirectToAction("Details", new { id = avaliacao.id });
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao salvar os dados.";
            }

            return View(avaliacao);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var avaliacao = await _repositoryAvaliacao.SelecionarChaveAsync(id);
            return View(avaliacao);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                await _repositoryAvaliacao.AlterarAsync(avaliacao);
                ViewData["Mensagem"] = "Dados alterados com sucesso.";
                return RedirectToAction("Details", new { id = avaliacao.id });
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao alterar os dados.";
                return View(avaliacao);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var avaliacao = await _repositoryAvaliacao.SelecionarChaveAsync(id);

            if (avaliacao == null)
            {
                return RedirectToAction("Create");
            }

            return View(avaliacao);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var avaliacao = await _repositoryAvaliacao.SelecionarChaveAsync(id);
            return View(avaliacao);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Avaliacao avaliacao)
        {
            await _repositoryAvaliacao.ExcluirAsync(avaliacao);
            return RedirectToAction("Index");
        }
    }
}
