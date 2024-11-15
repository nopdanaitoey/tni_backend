using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tni_back.Database;
using tni_back.Entities;
using tni_back.Repositories.IRepositories;

namespace tni_back.Repositories
{
    public class StockProductRepository : GenericRepository<StockProduct>, IStockProductRepository
    {
        public StockProductRepository(TniContext tniContext) : base(tniContext)
        {
        }
    }
}