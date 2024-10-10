using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class RepositoryFormacao : RepositoryBase<Formacao>
    {
        public RepositoryFormacao(db_BuscaEmpregoContext db, bool saveChanges = true) : base(db, saveChanges)
        {

        }
    }
}
