using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LymStyleWebAPPAPI.Domain.Contracts.Services
{
    public interface IBaseService<T> where T : class
    {
        List<T> GetAll();
        Task<T> GetById(int id);
        Task<int> Save(T entity);
        Task<int> Delete(int id);
        Task<int> SaveFile(int id, string filename);
    }
}
