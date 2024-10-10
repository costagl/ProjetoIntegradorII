using Model.Models;
using Model.Repositories;

namespace Model.Services
{
    public class ServiceVaga
    {
        private readonly RepositoryVaga _repositoryVaga;

        public ServiceVaga(RepositoryVaga repositoryVaga)
        {
            _repositoryVaga = repositoryVaga;
        }

        public async Task<List<Vaga>> ObterTodosVagasAsync()
        {
            return await _repositoryVaga.SelecionarTodosAsync();
        }

        public async Task<Vaga> ObterVagaPorIdAsync(int id)
        {
            return await _repositoryVaga.SelecionarChaveAsync(id);
        }

        public async Task IncluirVagaAsync(Vaga Vaga)
        {
            await _repositoryVaga.IncluirAsync(Vaga);
        }

        public async Task AlterarVagaAsync(Vaga Vaga)
        {
            await _repositoryVaga.AlterarAsync(Vaga);
        }

        public async Task ExcluirVagaAsync(Vaga Vaga)
        {
            await _repositoryVaga.ExcluirAsync(Vaga);
        }
    }
}