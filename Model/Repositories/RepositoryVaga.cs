﻿using Microsoft.EntityFrameworkCore;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repositories
{
    public class RepositoryVaga : RepositoryBase<Vaga>
    {
        public RepositoryVaga(db_BuscaEmpregoContext db, bool saveChanges = true) : base(db, saveChanges)
        {

        }
        public async Task<List<Vaga>> SelecionarTodosPorCnpjAsync(string cnpj)
        {
            var _db = new db_BuscaEmpregoContext();

            if (string.IsNullOrWhiteSpace(cnpj))
            {
                // Retorna todos os registros se o CNPJ não for fornecido
                return await _dbSet.ToListAsync();
            }

            // Filtra os registros pelo CNPJ
            return await _db.Vaga.Where(item => item.CNPJ_Empresa == cnpj).ToListAsync();
        }
    }
}
