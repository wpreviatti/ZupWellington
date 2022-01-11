using EFClient.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFClient.Repo
{
    public class EFCoreRepository : IEFCoreRepository
    {
        private readonly ZupContext _contexto;

        public EFCoreRepository(ZupContext contexto)
        {
            _contexto = contexto;
        }
        public void Add<T>(T entity) where T : class
        {
            _contexto.Add(entity);
        }

        public void Deletar<T>(T entity) where T : class
        {
            _contexto.Remove(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _contexto.Update(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _contexto.SaveChangesAsync()) > 0;
        }

        public async Task<IEnumerable<Cliente>> GetAll()
        {
            IQueryable<Cliente> query = _contexto.Cliente.Include(h => h.NomeLider).Include(h=>h.Telefone);
            query = query.AsNoTracking().OrderBy(or => or.NumeroChapaId);
            return await query.ToListAsync();
        }

        public async Task<Cliente> GetById(int id)
        {
            IQueryable<Cliente> query = _contexto.Cliente.Include(h => h.NomeLider).Include(h => h.Telefone);
            query = query.AsNoTracking().Where(w => w.NumeroChapaId == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Cliente>> GetByNome(string nome)
        {
            IQueryable<Cliente> query = _contexto.Cliente.Include(h => h.NomeLider).Include(h => h.Telefone);
            query = query.AsNoTracking().Where(w=>w.Nome.Contains(nome))
                .OrderBy(or => or.NumeroChapaId);
            return await query.ToListAsync();
        }
    }
}
