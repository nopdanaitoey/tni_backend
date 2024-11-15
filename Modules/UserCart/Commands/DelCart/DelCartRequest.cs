using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace tni_back.Modules.UserCart.Commands.DelCart
{
    public class DelCartRequest : IRequest<DelCartResponse>
    {
        public Guid UserId { get; set; }
        public Guid UserCartId { get; set; }
        public decimal Quantity { get; set; }
    }
}