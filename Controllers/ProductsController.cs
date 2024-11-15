using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using tni_back.Modules.MasterProducts.Queries.GetActiveProductList;
using tni_back.Responses;

namespace tni_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<BaseResponse> Get()
        {
            
            var result = await _mediator.Send(new GetActiveProductListRequest());

            return new BaseResponse().Success(result);
        }
    }
}