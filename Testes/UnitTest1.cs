using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Model.Models;
using Model.Repositories;

namespace Testes
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task SelecionarUserIdAsync()
        {
            using (var _db = new db_BuscaEmpregoContext())
            {
                var userId = "b88c2a48-b76a-49e5-9a57-dfe58c35003f"; // Remova os espaços
                var candidato = await _db.Candidato.FirstOrDefaultAsync(e => e.UserId == userId);

                if (candidato != null)
                {
                    var cpf = candidato.CPF; // Supondo que a propriedade se chama CPF
                                             // Aqui você pode fazer algo com o CPF, como retornar, logar ou afirmar em um teste.
                    
                }
                else
                {
                    
                }
            }
        }
    }
}