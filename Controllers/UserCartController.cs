using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using tni_back.Modules.UserCart.Commands.AddCart;
using tni_back.Modules.UserCart.Commands.DelCart;
using tni_back.Modules.UserCart.Queries.GetUserCart;
using tni_back.Responses;

namespace tni_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserCartController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserCartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<BaseResponse> Post([FromBody] AddCartRequest request)
        {
            var result = await _mediator.Send(request);
            if (!result.IsSuccess) return new BaseResponse().Fail(result.Message);
            return new BaseResponse().Success(result.Message);

        }

        [HttpGet("{userId}")]
        public async Task<BaseResponse> Get([FromRoute] Guid userId)
        {
            var result = await _mediator.Send(new GetUserCartRequest(userId));
            return new BaseResponse().Success(result);
        }
        [HttpDelete("{userId}/{userCartId}")]
        public async Task<BaseResponse> Get([FromRoute] Guid userId, [FromRoute] Guid userCartId)
        {
            DelCartRequest request = new DelCartRequest
            {
                UserCartId = userCartId,
                UserId = userId,
                Quantity = 1
            };
            var result = await _mediator.Send(request);
            return new BaseResponse().Success(result);
        }
    }
}