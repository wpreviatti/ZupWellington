using EFClient.Dominio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFClient.Repo
{
    public interface IEFCoreRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Deletar<T>(T entity) where T : class;

        Task< bool> SaveChangeAsync();
        Task<IEnumerable<Cliente>> GetAll();
        Task<Cliente> GetById(int id);
        Task<IEnumerable<Cliente>> GetByNome(string nome);
    }
}
