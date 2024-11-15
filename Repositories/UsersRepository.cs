using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tni_back.Database;
using tni_back.Entities;
using tni_back.Repositories.IRepositories;

namespace tni_back.Repositories
{
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        public UsersRepository(TniContext tniContext) : base(tniContext)
        {
        }
    }
}