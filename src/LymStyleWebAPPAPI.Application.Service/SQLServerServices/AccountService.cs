using LymStyleWebAPPAPI.Domain.Contracts.Repositories;
using LymStyleWebAPPAPI.Domain.Contracts.Services;
using LymStyleWebAPPAPI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LymStyleWebAPPAPI.Application.Service.SQLServerServices
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _user;
        public AccountService(IAccountRepository user)
        {
            _user = user;

        }
        public async Task<int> Delete(int id)
        {
            var entity = await _user.GetById(id);
            return await _user.Delete(entity);
        }
        public List<AccountDTO> GetAll()
        {
            return _user.GetAll().Select(user => new AccountDTO()
            {
                id = user.Id,
                name = user.Name,
                email = user.Email,
                password=user.Password,
            }).ToList();
        }

        public async Task<AccountDTO> GetById(int id)
        {
            var dto = new AccountDTO();
            return dto.mapToDTO(await _user.GetById(id));
        }

        public Task<int> Save(AccountDTO dto)
        {
            if (dto.id > 0)
            {
                return _user.Update(dto.mapToEntity());
            }
            else
            {
                return _user.Save(dto.mapToEntity());
            }
        }

        public Task<int> SaveFile(int id, string filename)
        {
            return _user.SaveFile(id, filename);
        }
    }
}
