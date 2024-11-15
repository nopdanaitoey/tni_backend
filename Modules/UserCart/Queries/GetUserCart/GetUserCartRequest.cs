using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace tni_back.Modules.UserCart.Queries.GetUserCart
{
    public sealed record GetUserCartRequest(Guid userId) : IRequest<GetUserCartResponse>;

}