using LymStyleWebAPPAPI.Domain.Contracts.Repositories;
using LymStyleWebAPPAPI.Infra.Data.Repository.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LymStyleWebAPPAPI.Infra.Data.Repository.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly SQLServerContext _sqlContext;
        public BaseRepository(SQLServerContext sqlContext)
        {
            _sqlContext = sqlContext;
        }
        public Task<int> Delete(T entity)
        {
            this._sqlContext.Set<T>().Remove(entity);
            return this._sqlContext.SaveChangesAsync();
        }

        public IQueryable<T> GetAll()
        {
            return this._sqlContext.Set<T>();
        }
        public async Task<T> GetById(int id)
        {
            return await this._sqlContext.Set<T>().FindAsync(id);
        }

        public Task<int> Save(T entity)
        {
            this._sqlContext.Set<T>().Add(entity);
            return this._sqlContext.SaveChangesAsync();
        }
        public Task<int> SaveFile(int id, string file)
        {
            throw new NotImplementedException();
        }
        public Task<int> Update(T entity)
        {
            try
            {
                this._sqlContext.Set<T>().Update(entity);
                return this._sqlContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var text = ex.Message;
                return this._sqlContext.SaveChangesAsync();
            }
        }
        public async Task<int> ExecuteCommand(string sqlCommand)
        {
            return await this._sqlContext.Database.ExecuteSqlRawAsync(sqlCommand);
        }
    }
}
