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
        private db_BuscaEmpregoContext _context;
        public RepositoryVaga _RepositoryVaga;

        public VagaController(db_BuscaEmpregoContext db)
        {
            _context = db;
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
                        select v;

            if (!String.IsNullOrEmpty(requisitos))
            {
                vagas = vagas.Where(v => v.Requisitos.Contains(requisitos));
            }
            if (!String.IsNullOrEmpty(areaAtuacao))
            {
                vagas = vagas.Where(v => v.AreaAtuacao.Contains(areaAtuacao));
            }
            if (!String.IsNullOrEmpty(modelo))
            {
                vagas = vagas.Where(v => v.Modelo.Contains(modelo));
            }
            if (!String.IsNullOrEmpty(localizacao))
            {
                vagas = vagas.Where(v => v.Localizacao.Contains(localizacao));
            }
            if (!String.IsNullOrEmpty(tipoContrato))
            {
                vagas = vagas.Where(v => v.TipoContrato.Contains(tipoContrato));
            }

            return View(await vagas.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
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

        public async Task<IActionResult> IndexVagasEmpresa(string cnpj)
        {
            return View(await _RepositoryVaga.SelecionarTodosPorCnpjAsync(cnpj));
        }
    }
}
