using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tni_back.Entities.Common;

namespace tni_back.Entities
{
    public class Users : BaseEntity
    {
        public string UserName { get; set; }
        public ICollection<UserCart> UserCarts { get; set; }
        public ICollection<StockProduct> StockProducts { get; set; }

    }
}