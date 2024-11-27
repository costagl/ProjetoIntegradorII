using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BuscaEmprego.Controllers
{
    [Authorize(Roles = "admin, empresa")]
    public class VagaController : Controller
    {
        private readonly db_BuscaEmpregoContext _context;
        private readonly RepositoryVaga _RepositoryVaga;
        private readonly UserManager<IdentityUser> _userManager;

        public VagaController(db_BuscaEmpregoContext db, UserManager<IdentityUser> userManager)
        {
            _context = db;
            _userManager = userManager;
            _RepositoryVaga = new RepositoryVaga(db);
        }
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _RepositoryVaga.SelecionarTodosAsync());
        //}

        [AllowAnonymous]
        public async Task<IActionResult> Index(string requisitos, string areaAtuacao, string modelo, string localizacao, string tipoContrato)
        {
            var vagas = from v in _context.Vaga
                        .Include(v => v.CNPJ_EmpresaNavigation) // Inclui a navegação para a tabela Empresa
                        select v;

            if (!string.IsNullOrEmpty(requisitos))
                vagas = vagas.Where(v => v.Requisitos.Contains(requisitos));
            if (!string.IsNullOrEmpty(areaAtuacao))
                vagas = vagas.Where(v => v.AreaAtuacao.Contains(areaAtuacao));
            if (!string.IsNullOrEmpty(modelo))
                vagas = vagas.Where(v => v.Modelo.Contains(modelo));
            if (!string.IsNullOrEmpty(localizacao))
                vagas = vagas.Where(v => v.Localizacao.Contains(localizacao));
            if (!string.IsNullOrEmpty(tipoContrato))
                vagas = vagas.Where(v => v.TipoContrato.Contains(tipoContrato));


            return View(await vagas.ToListAsync());
        }


        public async Task<IActionResult> IndexVagaEmpresa(string cnpj)
        {
            return View(await _RepositoryVaga.SelecionarTodosPorCnpjAsync(cnpj));
        }

        public IActionResult Create()
        {
            var userId = _userManager.GetUserId(User);

            var _repositoryEmpresa = new RepositoryEmpresa(_context);
            var candidato = _repositoryEmpresa.SelecionarUserId(userId);

            var vaga = new Vaga { CNPJ_Empresa = candidato.CNPJ };

            if (vaga.CNPJ_Empresa == null)
            {
                throw new ArgumentNullException(nameof(vaga));
            }

            return View(vaga);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vaga vaga)
        {
            if (ModelState.IsValid)
            {
                await _RepositoryVaga.IncluirAsync(vaga);
                ViewData["Mensagem"] = "Dados salvos com sucesso.";
                return RedirectToAction("IndexVagasEmpresa", new { cnpj = vaga.CNPJ_Empresa });
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao salvar os dados.";
            }

            return View(vaga);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var vaga = await _RepositoryVaga.SelecionarChaveAsync(id);
            return View(vaga);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vaga vaga)
        {
            if (ModelState.IsValid)
            {
                await _RepositoryVaga.AlterarAsync(vaga);
                return RedirectToAction("Index");
            }
            else
            {
                return View(vaga);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vaga = await _RepositoryVaga.SelecionarChaveAsync(id);
            return View(vaga);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Vaga vaga)
        {
            await _RepositoryVaga.ExcluirAsync(vaga);
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _RepositoryVaga.SelecionarChaveAsync(id));
        }
    }
}
