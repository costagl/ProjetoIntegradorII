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
    public class FormacaoController : Controller
    {
        private readonly db_BuscaEmpregoContext _context;
        private readonly RepositoryFormacao _repositoryFormacao;
        private readonly UserManager<IdentityUser> _userManager;

        public FormacaoController(db_BuscaEmpregoContext db, UserManager<IdentityUser> userManager)
        {
            _context = db;
            _userManager = userManager;
            _repositoryFormacao = new RepositoryFormacao(db);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repositoryFormacao.SelecionarTodosAsync());
        }

        public async Task<IActionResult> IndexListaPorCPF(string cpf)
        {
            return View(await _repositoryFormacao.SelecionarTodosPorCPFAsync(cpf));
        }

        public IActionResult Create(string cpf)
        {
            var formacao = new Formacao { CPF_Candidato = cpf };
            return View(formacao);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Formacao formacao)
        {
            if (ModelState.IsValid)
            {
                await _repositoryFormacao.IncluirAsync(formacao);
                ViewData["Mensagem"] = "Dados salvos com sucesso.";
                return RedirectToAction("Details", new { id = formacao.id });
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao salvar os dados.";
            }

            return View(formacao);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var formacao = await _repositoryFormacao.SelecionarChaveAsync(id);
            return View(formacao);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Formacao formacao)
        {
            if (ModelState.IsValid)
            {
                await _repositoryFormacao.AlterarAsync(formacao);
                ViewData["Mensagem"] = "Dados alterados com sucesso.";
                return RedirectToAction("Details", new { id = formacao.id });
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao alterar os dados.";
                return View(formacao);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var formacao = await _repositoryFormacao.SelecionarChaveAsync(id);

            if (formacao == null)
            {
                return RedirectToAction("Create");
            }

            return View(formacao);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var formacao = await _repositoryFormacao.SelecionarChaveAsync(id);
            return View(formacao);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Formacao formacao)
        {
            await _repositoryFormacao.ExcluirAsync(formacao);
            return RedirectToAction("Index");
        }
    }
}
