using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Repositories;

namespace BuscaEmprego.Controllers
{
    [Authorize]
    public class CandidatoController : DefaultController
    {
        private db_BuscaEmpregoContext _context;
        public RepositoryCandidato _RepositoryCandidato;

        public CandidatoController(db_BuscaEmpregoContext db)
        {
            _context = db;
            _RepositoryCandidato = new RepositoryCandidato(db);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _RepositoryCandidato.SelecionarTodosAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Candidato candidato)
        {
            if (ModelState.IsValid)
            {
                await _RepositoryCandidato.IncluirAsync(candidato);
                ViewData["Mensagem"] = "Dados salvos com sucesso.";
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao salvar os dados.";
            }
            return View(candidato);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var Candidato = await _RepositoryCandidato.SelecionarChaveAsync(id);
            return View(Candidato);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Candidato candidato)
        {

            if (ModelState.IsValid)
            {

                await _RepositoryCandidato.AlterarAsync(candidato);
                ViewData["Mensagem"] = "Dados alterados com sucesso.";
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao alterar os dados.";
            }
            return View(candidato);
        }
        public async Task<IActionResult> Details(string cpf)
        {
            return View(await _RepositoryCandidato.SelecionarChaveAsync("17873426700"));
        }

        public async Task<IActionResult> Delete(int id)
        {

            var unidade = await _RepositoryCandidato.SelecionarChaveAsync(id);
            return View(unidade);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Candidato candidato)
        {
            await _RepositoryCandidato.ExcluirAsync(candidato);
            return RedirectToAction("Index");
        }

    }
}
