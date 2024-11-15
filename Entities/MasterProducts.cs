using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tni_back.Entities.Common;

namespace tni_back.Entities
{
    public class MasterProducts : BaseEntity
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string? ImgUrl { get; set; } = null;
        public decimal Price { get; set; }
        public ICollection<StockProduct> StockProducts { get; set; }
        public ICollection<UserCart> UserCarts { get; set; }
    }
}