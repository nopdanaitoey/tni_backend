using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using tni_back.Repositories.IRepositories;

namespace tni_back.Modules.UserCart.Queries.GetUserCart
{
    public class GetUserCartRequestHandler : IRequestHandler<GetUserCartRequest, GetUserCartResponse>
    {
        private readonly IUserCartRepository _userCartRepository;

        public GetUserCartRequestHandler(IUserCartRepository userCartRepository)
        {
            _userCartRepository = userCartRepository;
        }

        public async Task<GetUserCartResponse> Handle(GetUserCartRequest request, CancellationToken cancellationToken)
        {
            GetUserCartResponse result = new GetUserCartResponse
            {

                GrandTotalPrice = 0,
                UserCartDetail = new List<GetUserCartDetail>()

            };
            var existUserCart = await _userCartRepository.GetByUserIdAsync(request.userId);
            if (existUserCart is not null && existUserCart.Count > 0)
            {
                result.UserCartDetail = new List<GetUserCartDetail>();
                var grouped = existUserCart.GroupBy(x => x.ProductId);
                foreach (var item in grouped)
                {
                    result.UserCartDetail.Add(new GetUserCartDetail
                    {
                        UserCartId = item.FirstOrDefault().Id,
                        ProductId = item.Key,
                        ProductName = item.FirstOrDefault().MasterProducts.Name,
                        ImgUrl = item.FirstOrDefault().MasterProducts.ImgUrl,
                        Price = item.FirstOrDefault().MasterProducts.Price,
                        Quantity = item.Sum(x => x.Quantity),

                    });
                }
                result.GrandTotalQuantity = existUserCart.Sum(x => x.Quantity);
                result.GrandTotalPrice = existUserCart.Sum(x => x.TotalPrice);
            }

            return result;
        }
    }
}