using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Model.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace BuscaEmprego.Controllers
{
    public class VagaController : DefaultController
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

        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _RepositoryVaga.SelecionarChaveAsync(id));
        }
    }
}
