using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class RepositoryCandidatura : RepositoryBase<Candidatura>
    {
        public RepositoryCandidatura(db_BuscaEmpregoContext db, bool saveChanges = true) : base(db, saveChanges)
        {

        }
        public async Task<List<Candidatura>> SelecionarCandidaturasPorEmpresa(string cnpjEmpresa)
        {
            var _db = new db_BuscaEmpregoContext();

            var candidaturas = await _db.Candidatura
                .Include(c => c.idVagaNavigation)
                .ThenInclude(v => v.CNPJ_EmpresaNavigation)
                .Where(c => c.idVagaNavigation.CNPJ_EmpresaNavigation.CNPJ == cnpjEmpresa)
                .ToListAsync();

            return candidaturas;
        }

        public async Task<List<Candidatura>> SelecionarTodosPorCPFAsync(string cpf)
        {
            var _db = new db_BuscaEmpregoContext();

            if (string.IsNullOrWhiteSpace(cpf))
            {
                return await _dbSet.ToListAsync();
            }

            return await _db.Candidatura.Where(item => item.CPF_Candidato == cpf).ToListAsync();
        }
        public async Task<Candidatura> SelecionarPorCPFAsync(string cpf)
        {
            var _db = new db_BuscaEmpregoContext();
            var candidato = await _db.Candidatura.FirstOrDefaultAsync(e => e.CPF_Candidato == cpf);

            return candidato;
        }
    }
}
