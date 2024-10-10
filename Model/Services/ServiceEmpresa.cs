using Model.Models;
using Model.Repositories;

namespace Model.Services
{
    public class ServiceEmpresa
    {
        private readonly RepositoryEmpresa _repositoryEmpresa;

        public ServiceEmpresa(RepositoryEmpresa repositoryEmpresa)
        {
            _repositoryEmpresa = repositoryEmpresa;
        }

        public async Task<List<Empresa>> ObterTodosEmpresasAsync()
        {
            return await _repositoryEmpresa.SelecionarTodosAsync();
        }

        public async Task<Empresa> ObterEmpresaPorIdAsync(int id)
        {
            return await _repositoryEmpresa.SelecionarChaveAsync(id);
        }

        public async Task IncluirEmpresaAsync(Empresa Empresa)
        {
            await _repositoryEmpresa.IncluirAsync(Empresa);
        }

        public async Task AlterarEmpresaAsync(Empresa Empresa)
        {
            await _repositoryEmpresa.AlterarAsync(Empresa);
        }

        public async Task ExcluirEmpresaAsync(Empresa Empresa)
        {
            await _repositoryEmpresa.ExcluirAsync(Empresa);
        }
    }
}