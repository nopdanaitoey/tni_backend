using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using tni_back.Database;
using tni_back.Entities;
using tni_back.Repositories.IRepositories;

namespace tni_back.Repositories
{
    public class UserCartRepositroy : GenericRepository<UserCart>, IUserCartRepository
    {
        private readonly TniContext _tniContext;
        public UserCartRepositroy(TniContext tniContext) : base(tniContext)
        {
            _tniContext = tniContext;
        }

        public async Task<UserCart> GetByUserIdAndProductIdAsync(Guid userId, Guid productId, bool? isActive = true, bool? isDeleted = false)
        {
            var result = await _tniContext.UserCarts
                                .Where(x => x.UserId == userId
                                && x.IsActive == isActive
                                && x.IsDeleted == isDeleted
                                && x.ProductId == productId
                                 )
                                .Include(x => x.MasterProducts)
                                .Include(x => x.Users)
                                .AsNoTracking()
                                .FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<UserCart>> GetByUserIdAsync(Guid userId, bool? isActive = true, bool? isDeleted = false)
        {
            var result = await _tniContext.UserCarts
                                .Where(x => x.UserId == userId
                                && x.IsActive == isActive
                                && x.IsDeleted == isDeleted)
                                .Include(x => x.MasterProducts)
                                .Include(x => x.Users)
                                .AsNoTracking()
                                .ToListAsync();
            return result;
        }
    }
}