using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> IncluirAsync(T obj);

        Task<T> AlterarAsync(T obj);

        Task ExcluirAsync(T obj);

        Task ExcluirAsync(params object[] obj);

        Task<T> SelecionarChaveAsync(params object[] obj);

        Task<List<T>> SelecionarTodosAsync();
    }
}
