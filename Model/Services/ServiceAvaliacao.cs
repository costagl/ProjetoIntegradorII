using Model.Models;
using Model.Repositories;

namespace Model.Services
{
    public class ServiceAvaliacao
    {
        private readonly RepositoryAvaliacao _repositoryAvaliacao;

        public ServiceAvaliacao(RepositoryAvaliacao repositoryAvaliacao)
        {
            _repositoryAvaliacao = repositoryAvaliacao;
        }

        public async Task<List<Avaliacao>> ObterTodosAvaliacaosAsync()
        {
            return await _repositoryAvaliacao.SelecionarTodosAsync();
        }

        public async Task<Avaliacao> ObterAvaliacaoPorIdAsync(int id)
        {
            return await _repositoryAvaliacao.SelecionarChaveAsync(id);
        }

        public async Task IncluirAvaliacaoAsync(Avaliacao Avaliacao)
        {
            await _repositoryAvaliacao.IncluirAsync(Avaliacao);
        }

        public async Task AlterarAvaliacaoAsync(Avaliacao Avaliacao)
        {
            await _repositoryAvaliacao.AlterarAsync(Avaliacao);
        }

        public async Task ExcluirAvaliacaoAsync(Avaliacao Avaliacao)
        {
            await _repositoryAvaliacao.ExcluirAsync(Avaliacao);
        }
    }
}