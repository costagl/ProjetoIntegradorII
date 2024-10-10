using Model.Models;
using Model.Repositories;

namespace Model.Services
{
    public class ServiceCandidatura
    {
        private readonly RepositoryCandidatura _repositoryCandidatura;

        public ServiceCandidatura(RepositoryCandidatura repositoryCandidatura)
        {
            _repositoryCandidatura = repositoryCandidatura;
        }

        public async Task<List<Candidatura>> ObterTodosCandidaturasAsync()
        {
            return await _repositoryCandidatura.SelecionarTodosAsync();
        }

        public async Task<Candidatura> ObterCandidaturaPorIdAsync(int id)
        {
            return await _repositoryCandidatura.SelecionarChaveAsync(id);
        }

        public async Task IncluirCandidaturaAsync(Candidatura Candidatura)
        {
            await _repositoryCandidatura.IncluirAsync(Candidatura);
        }

        public async Task AlterarCandidaturaAsync(Candidatura Candidatura)
        {
            await _repositoryCandidatura.AlterarAsync(Candidatura);
        }

        public async Task ExcluirCandidaturaAsync(Candidatura Candidatura)
        {
            await _repositoryCandidatura.ExcluirAsync(Candidatura);
        }
    }
}