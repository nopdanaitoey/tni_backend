using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace tni_back.Modules.UserCart.Commands.AddCart
{
    public class AddCartRequest : IRequest<AddCartResponse>
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Quantity { get; set; }
    }
}