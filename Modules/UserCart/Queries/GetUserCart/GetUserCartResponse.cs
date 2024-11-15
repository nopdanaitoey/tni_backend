using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace tni_back.Modules.UserCart.Queries.GetUserCart
{
    public class GetUserCartResponse
    {
        public decimal GrandTotalPrice { get; set; }
        public decimal GrandTotalQuantity { get; set; }
        public List<GetUserCartDetail> UserCartDetail { get; set; } = new List<GetUserCartDetail>();
    }

    public class GetUserCartDetail
    {

        public Guid UserCartId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImgUrl { get; set; }
        public decimal Price { get; set; }
        public decimal Quantity { get; set; }

    }


}