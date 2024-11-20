using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class RepositoryExperiencia : RepositoryBase<Experiencia>
    {
        public RepositoryExperiencia(db_BuscaEmpregoContext db, bool saveChanges = true) : base(db, saveChanges)
        {

        }

        public async Task<List<Experiencia>> SelecionarTodosPorCPFAsync(string cpf)
        {
            var _db = new db_BuscaEmpregoContext();

            if (string.IsNullOrWhiteSpace(cpf))
            {
                return await _dbSet.ToListAsync();
            }

            return await _db.Experiencia.Where(item => item.CPF_Candidato == cpf).ToListAsync();
        }
    }
}
