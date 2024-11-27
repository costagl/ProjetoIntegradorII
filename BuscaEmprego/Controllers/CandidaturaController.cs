using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Model.Models;
using Model.Repositories;

namespace BuscaEmprego.Controllers
{
    public class CandidaturaController : Controller
    {
        private readonly db_BuscaEmpregoContext _context;
        private readonly RepositoryCandidatura _repositoryCandidatura;
        private readonly UserManager<IdentityUser> _userManager;

        public CandidaturaController(db_BuscaEmpregoContext db, UserManager<IdentityUser> userManager)
        {
            _context = db;
            _userManager = userManager;
            _repositoryCandidatura = new RepositoryCandidatura(db);
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repositoryCandidatura.SelecionarTodosAsync());
        }

        [Authorize(Roles = "admin, empresa")]
        public async Task<IActionResult> IndexCandidaturaEmpresa(string cnpj)
        {
            return View(await _repositoryCandidatura.SelecionarCandidaturasPorEmpresa(cnpj));
        }

        public async Task<IActionResult> IndexListaPorCPF(string cpf)
        {
            return View(await _repositoryCandidatura.SelecionarTodosPorCPFAsync(cpf));
        }

        public IActionResult Create(int idVaga)
        {
            var userId = _userManager.GetUserId(User);

            var _repositoryCandidato = new RepositoryCandidato(_context);
            var _repositoryVaga = new RepositoryVaga(_context);

            var candidato = _repositoryCandidato.SelecionarUserId(userId);

            if (candidato == null)
            {
                throw new ArgumentNullException("Usuário não encontrado ou não possui um CPF associado.");
            }

            var candidatura = new Candidatura
            {
                CPF_Candidato = candidato.CPF,
                idVaga = idVaga 
            };

            return View(candidatura);
        }


        [HttpPost]
        public async Task<IActionResult> Create(Candidatura candidatura, IFormFile curriculo)
        {
            if (ModelState.IsValid)
            {
                // Verificar se um arquivo foi enviado
                if (curriculo != null && curriculo.Length > 0)
                {
                    // Processar o arquivo e salvar no banco
                    using (var memoryStream = new MemoryStream())
                    {
                        await curriculo.CopyToAsync(memoryStream);  // Copiar o arquivo para o MemoryStream
                        candidatura.Curriculo = memoryStream.ToArray(); // Converter o arquivo em byte[] e atribuir à propriedade Curriculo
                    }
                }

                // Salvar os dados da candidatura no repositório
                await _repositoryCandidatura.IncluirAsync(candidatura);

                // Exibir uma mensagem de sucesso
                ViewData["Mensagem"] = "Dados salvos com sucesso.";

                // Redirecionar para a página de detalhes da candidatura
                return RedirectToAction("Details", new { id = candidatura.id });
            }
            else
            {
                // Exibir mensagem de erro caso o modelo não seja válido
                ViewData["MensagemErro"] = "Ocorreu um erro ao salvar os dados.";
            }

            // Retornar a view com o modelo de candidatura
            return View(candidatura);
        }



        public async Task<IActionResult> Edit(int id)
        {
            var candidatura = await _repositoryCandidatura.SelecionarChaveAsync(id);
            return View(candidatura);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Candidatura candidatura)
        {
            if (ModelState.IsValid)
            {
                await _repositoryCandidatura.AlterarAsync(candidatura);
                ViewData["Mensagem"] = "Dados alterados com sucesso.";
                return RedirectToAction("Details", new { id = candidatura.id });
            }
            else
            {
                ViewData["MensagemErro"] = "Ocorreu um erro ao alterar os dados.";
                return View(candidatura);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var candidatura = await _repositoryCandidatura.SelecionarChaveAsync(id);

            //if (candidatura == null)
            //{
            //    return RedirectToAction("Create");
            //}

            return View(candidatura);
        }


        public async Task<IActionResult> Delete(int id)
        {
            var candidatura = await _repositoryCandidatura.SelecionarChaveAsync(id);
            return View(candidatura);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Candidatura candidatura)
        {
            await _repositoryCandidatura.ExcluirAsync(candidatura);
            return RedirectToAction("Index");
        }
    }
}
