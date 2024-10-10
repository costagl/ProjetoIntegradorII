using Microsoft.EntityFrameworkCore;
using Model.Interfaces;
using Model.Models;

namespace Model.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>, IDisposable where T : class
    {
        public db_BuscaEmpregoContext _db;
        public DbSet<T> _dbSet;
        public bool _saveChanges = true;

        public RepositoryBase(db_BuscaEmpregoContext db, bool saveChanges)
        {
            _db = db;
            _saveChanges = saveChanges;
            _dbSet = _db.Set<T>();
        }

        public async Task<T> AlterarAsync(T obj)
        {
            _dbSet.Entry(obj).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return obj;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public async Task ExcluirAsync(T obj)
        {
            _dbSet.Entry(obj).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
        }

        public async Task ExcluirAsync(params object[] obj)
        {
            var x = await _dbSet.FindAsync(obj);
            await ExcluirAsync(x!);
        }

        public async Task<T> IncluirAsync(T obj)
        {
            await _dbSet.AddAsync(obj);
            await _db.SaveChangesAsync();
            return obj;
        }

        public async Task<T> SelecionarChaveAsync(params object[] obj)
        {
            return await _dbSet.FindAsync(obj);
        }

        public async Task<List<T>> SelecionarTodosAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
