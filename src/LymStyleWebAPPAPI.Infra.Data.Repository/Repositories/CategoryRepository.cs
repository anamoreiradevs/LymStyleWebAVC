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
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly SQLServerContext _context;
        public CategoryRepository(SQLServerContext context) : base(context)
        {
        }

    }
}
