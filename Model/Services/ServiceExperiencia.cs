using Model.Models;
using Model.Repositories;

namespace Model.Services
{
    public class ServiceExperiencia
    {
        private readonly RepositoryExperiencia _repositoryExperiencia;

        public ServiceExperiencia(RepositoryExperiencia repositoryExperiencia)
        {
            _repositoryExperiencia = repositoryExperiencia;
        }

        public async Task<List<Experiencia>> ObterTodosExperienciasAsync()
        {
            return await _repositoryExperiencia.SelecionarTodosAsync();
        }

        public async Task<Experiencia> ObterExperienciaPorIdAsync(int id)
        {
            return await _repositoryExperiencia.SelecionarChaveAsync(id);
        }

        public async Task IncluirExperienciaAsync(Experiencia Experiencia)
        {
            await _repositoryExperiencia.IncluirAsync(Experiencia);
        }

        public async Task AlterarExperienciaAsync(Experiencia Experiencia)
        {
            await _repositoryExperiencia.AlterarAsync(Experiencia);
        }

        public async Task ExcluirExperienciaAsync(Experiencia Experiencia)
        {
            await _repositoryExperiencia.ExcluirAsync(Experiencia);
        }
    }
}