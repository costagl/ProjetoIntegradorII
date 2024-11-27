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
    public class ExperienciaController : Controller
    {
        private readonly db_BuscaEmpregoContext _context;
        private readonly RepositoryExperiencia _repositoryExperiencia;
        private readonly UserManager<IdentityUser> _userManager;

        public ExperienciaController(db_BuscaEmpregoContext db, UserManager<IdentityUser> userManager)
        {
            _context = db;
            _userManager = userManager;
            _repositoryExperiencia = new RepositoryExperiencia(db);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repositoryExperiencia.SelecionarTodosAsync());
        }

        public async Task<IActionResult> IndexListaPorCPF(string cpf)
        {
            return View(await _repositoryExperiencia.SelecionarTodosPorCPFAsync(cpf));
        }

        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);

            var _repositoryCandidato = new RepositoryCandidato(_context);
            var candidato = _repositoryCandidato.SelecionarUserId(userId);

            var experiencia = new Experiencia { CPF_Candidato = candidato.CPF };

            if (experiencia.CPF_Candidato == null)
            {
                throw new ArgumentNullException(nameof(experiencia));
            }

            return View(experiencia);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Experiencia experiencia)
        {
            if (ModelState.IsValid)
            {
                await _repositoryExperiencia.IncluirAsync(experiencia);
                ViewData["Mensagem"] = "Dados salvos com sucesso.";
                return RedirectToAction("Details", new { id = experiencia.id });
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao salvar os dados.";
            }

            return View(experiencia);
        }


        public async Task<IActionResult> Edit(int id)
        {
            var experiencia = await _repositoryExperiencia.SelecionarChaveAsync(id);
            return View(experiencia);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Experiencia experiencia)
        {
            if (ModelState.IsValid)
            {
                await _repositoryExperiencia.AlterarAsync(experiencia);
                ViewData["Mensagem"] = "Dados alterados com sucesso.";
                return RedirectToAction("Details", new { id = experiencia.id });
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao alterar os dados.";
                return View(experiencia);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var experiencia = await _repositoryExperiencia.SelecionarChaveAsync(id);

            if (experiencia == null)
            {
                return RedirectToAction("Create");
            }

            return View(experiencia);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var experiencia = await _repositoryExperiencia.SelecionarChaveAsync(id);
            return View(experiencia);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Experiencia experiencia)
        {
            await _repositoryExperiencia.ExcluirAsync(experiencia);
            return RedirectToAction("Index");
        }
    }
}
