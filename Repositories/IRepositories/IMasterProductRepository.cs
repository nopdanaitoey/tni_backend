using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tni_back.Entities;

namespace tni_back.Repositories.IRepositories
{
    public interface IMasterProductRepository : IGenericRepository<MasterProducts>
    {
        Task<List<MasterProducts>> GetProductsByIsActiveAsync(bool? isActive = true,bool? isDeleted = false);
    }
}