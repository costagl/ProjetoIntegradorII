using Model.Models;
using Model.Repositories;

namespace Model.Services
{
    public class ServiceCandidato
    {
        private readonly RepositoryCandidato _repositoryCandidato;

        public ServiceCandidato(RepositoryCandidato repositoryCandidato)
        {
            _repositoryCandidato = repositoryCandidato;
        }

        public async Task<List<Candidato>> ObterTodosCandidatosAsync()
        {
            return await _repositoryCandidato.SelecionarTodosAsync();
        }

        public async Task<Candidato> ObterCandidatoPorIdAsync(int id)
        {
            return await _repositoryCandidato.SelecionarChaveAsync(id);
        }

        public async Task IncluirCandidatoAsync(Candidato candidato)
        {
            await _repositoryCandidato.IncluirAsync(candidato);
        }

        public async Task AlterarCandidatoAsync(Candidato candidato)
        {
            await _repositoryCandidato.AlterarAsync(candidato);
        }

        public async Task ExcluirCandidatoAsync(Candidato candidato)
        {
            await _repositoryCandidato.ExcluirAsync(candidato);
        }
    }
}