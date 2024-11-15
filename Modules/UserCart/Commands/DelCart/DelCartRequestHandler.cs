using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using tni_back.Entities;
using tni_back.Entities.Constants;
using tni_back.Repositories.IRepositories;

namespace tni_back.Modules.UserCart.Commands.DelCart
{
    public class DelCartRequestHandler : IRequestHandler<DelCartRequest, DelCartResponse>
    {
        private readonly IUserCartRepository _userCartRepository;
        private readonly IStockProductRepository _stockProductRepository;
        private readonly IMasterProductRepository _masterProductRepository;
        private readonly IUsersRepository _usersRepository;
        public DelCartRequestHandler(
            IUserCartRepository userCartRepository,
            IStockProductRepository stockProductRepository,
            IMasterProductRepository masterProductRepository,
            IUsersRepository usersRepository
        )
        {
            _userCartRepository = userCartRepository;
            _stockProductRepository = stockProductRepository;
            _masterProductRepository = masterProductRepository;
            _usersRepository = usersRepository;
        }

        public async Task<DelCartResponse> Handle(DelCartRequest request, CancellationToken cancellationToken)
        {
            DelCartResponse result = new DelCartResponse();
            var existUser = await _usersRepository.Get(request.UserId);
            if (existUser is null) throw new Exception("User Not Found");
            var existMyCart = await _userCartRepository.Get(request.UserCartId);
            if (existMyCart is null) throw new Exception("Product Not Found");

            var existMasterProduct = await _masterProductRepository.Get(existMyCart.ProductId);
            if (existMasterProduct is null) throw new Exception("Product Not Found");

            existMyCart.Quantity -= request.Quantity;
            existMyCart.TotalPrice = existMyCart.Quantity * existMyCart.MasterProducts.Price;
            if (existMyCart.Quantity <= 0) existMyCart.IsDeleted = ConstantsEntity.DELETED;
            await _userCartRepository.Update(existMyCart);

            existMasterProduct.Quantity += request.Quantity;
            await _masterProductRepository.Update(existMasterProduct);



            StockProduct stockProduct = request.Adapt<StockProduct>();
            stockProduct.ProductId = existMyCart.ProductId;
            stockProduct.StockTypeId = ConstantsEntity.REMOVE_FROM_CART;

            await _stockProductRepository.Add(stockProduct);
            result.IsSuccess = true;
            result.Message = "Product deleted from cart successfully";
            return result;
        }
    }
}