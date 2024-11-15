using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tni_back.Entities;

namespace tni_back.Repositories.IRepositories
{
    public interface IUserCartRepository : IGenericRepository<UserCart>
    {
        Task<List<UserCart>> GetByUserIdAsync(Guid userId, bool? isActive = true, bool? isDeleted = false);
        Task<UserCart> GetByUserIdAndProductIdAsync(Guid userId, Guid productId, bool? isActive = true, bool? isDeleted = false);
    }
}