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
    public class MasterProductRepository : GenericRepository<MasterProducts>, IMasterProductRepository
    {
        private TniContext _tniContext;
        public MasterProductRepository(TniContext tniContext) : base(tniContext)
        {
            _tniContext = tniContext;
        }

        public async Task<List<MasterProducts>> GetProductsByIsActiveAsync(bool? isActive = true, bool? isDeleted = false)
        {
            return await _tniContext.MasterProducts.Where(x => x.IsActive == isActive && x.IsDeleted == isDeleted).AsNoTracking().ToListAsync();
        }
    }
}