using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace tni_back.Modules.MasterProducts.Queries.GetActiveProductList
{
    public sealed record GetActiveProductListRequest : IRequest<List<GetActiveProductListResponse>>
    {
        
    }
    
}