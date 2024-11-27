using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class RepositoryEmpresa : RepositoryBase<Empresa>
    {
        public RepositoryEmpresa(db_BuscaEmpregoContext db, bool saveChanges = true) : base(db, saveChanges)
        {

        }
        public Empresa SelecionarUserId(string userId)
        {
            var _db = new db_BuscaEmpregoContext();
            var empresa = _db.Empresa.FirstOrDefault(e => e.UserId == userId);

            return empresa;
        }
        public async Task<Empresa> SelecionarUserIdAsync(string userId)
        {
            var _db = new db_BuscaEmpregoContext();
            var empresa = await _db.Empresa.FirstOrDefaultAsync(e => e.UserId == userId);

            return empresa;
        }
    }
}
