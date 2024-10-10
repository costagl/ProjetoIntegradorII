using Model.Models;
using Model.Repositories;

namespace Model.Services
{
    public class ServiceFormacao
    {
        private readonly RepositoryFormacao _repositoryFormacao;

        public ServiceFormacao(RepositoryFormacao repositoryFormacao)
        {
            _repositoryFormacao = repositoryFormacao;
        }

        public async Task<List<Formacao>> ObterTodosFormacaosAsync()
        {
            return await _repositoryFormacao.SelecionarTodosAsync();
        }

        public async Task<Formacao> ObterFormacaoPorIdAsync(int id)
        {
            return await _repositoryFormacao.SelecionarChaveAsync(id);
        }

        public async Task IncluirFormacaoAsync(Formacao Formacao)
        {
            await _repositoryFormacao.IncluirAsync(Formacao);
        }

        public async Task AlterarFormacaoAsync(Formacao Formacao)
        {
            await _repositoryFormacao.AlterarAsync(Formacao);
        }

        public async Task ExcluirFormacaoAsync(Formacao Formacao)
        {
            await _repositoryFormacao.ExcluirAsync(Formacao);
        }
    }
}