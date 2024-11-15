using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using tni_back.Repositories.IRepositories;

namespace tni_back.Modules.MasterProducts.Queries.GetActiveProductList
{
    public class GetActiveProductListRequestHandler : IRequestHandler<GetActiveProductListRequest, List<GetActiveProductListResponse>>
    {
        private readonly IMasterProductRepository _masterProductRepository;
        public GetActiveProductListRequestHandler(IMasterProductRepository masterProductRepository)
        {
            _masterProductRepository = masterProductRepository;
        }

        public async Task<List<GetActiveProductListResponse>> Handle(GetActiveProductListRequest request, CancellationToken cancellationToken)
        {
            List<GetActiveProductListResponse> result = new List<GetActiveProductListResponse>();
            var existActiveProducts = await _masterProductRepository.GetProductsByIsActiveAsync();
            if (existActiveProducts is not null && existActiveProducts.Count > 0) result = existActiveProducts.Adapt<List<GetActiveProductListResponse>>();
            return result;
        }
    }
}