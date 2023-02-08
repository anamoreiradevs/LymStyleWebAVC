using LymStyleWebAPPAPI.Domain.Contracts.Repositories;
using LymStyleWebAPPAPI.Domain.Entities;
using LymStyleWebAPPAPI.Infra.Data.Repository.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LymStyleWebAPPAPI.Infra.Data.Repository.Repositories
{
    public class AccountRepository : BaseRepository<Account>, IAccountRepository
    {
        private readonly SQLServerContext _context;
        public AccountRepository(SQLServerContext context) : base(context)
        {
            this._context = context;
        }
        public async Task<int> SaveFile(int id, string fileName)
        {
            string sqlCommand = $"UPDATE [dbo].[Users] SET Image = '{fileName}' WHERE Id = {id}";
            return await this.ExecuteCommand(sqlCommand);
        }
    }
}
