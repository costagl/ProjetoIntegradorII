using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class RepositoryCandidato : RepositoryBase<Candidato>
    {
        public RepositoryCandidato(db_BuscaEmpregoContext db, bool saveChanges = true) : base(db, saveChanges)
        {

        }
        public async Task<Candidato> SelecionarUserIdAsync(string userId)
        {
            var _db = new db_BuscaEmpregoContext();
            var candidato = await _db.Candidato.FirstOrDefaultAsync(e => e.UserId == userId);

            return candidato;
        }
    }
}
