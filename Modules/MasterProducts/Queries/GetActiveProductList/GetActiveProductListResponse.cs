using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tni_back.Modules.MasterProducts.Queries.GetActiveProductList
{
    public class GetActiveProductListResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public string? ImgUrl { get; set; } = null;
        public decimal Price { get; set; }
    }
}