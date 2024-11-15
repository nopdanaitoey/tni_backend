using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using MediatR;
using tni_back.Entities;
using tni_back.Entities.Constants;
using tni_back.Repositories.IRepositories;

namespace tni_back.Modules.UserCart.Commands.AddCart
{
    public class AddCartRequestHandler : IRequestHandler<AddCartRequest, AddCartResponse>
    {
        private readonly IUserCartRepository _userCartRepository;
        private readonly IMasterProductRepository _masterProductRepository;
        private readonly IStockProductRepository _stockProductRepository;
        private readonly IUsersRepository _usersRepository;

        public AddCartRequestHandler(IUserCartRepository userCartRepository,
            IMasterProductRepository masterProductRepository,
            IStockProductRepository stockProductRepository,
            IUsersRepository usersRepository)
        {
            _userCartRepository = userCartRepository;
            _masterProductRepository = masterProductRepository;
            _stockProductRepository = stockProductRepository;
            _usersRepository = usersRepository;
        }

        public async Task<AddCartResponse> Handle(AddCartRequest request, CancellationToken cancellationToken)
        {
            AddCartResponse result = new AddCartResponse();

            var existUser = await _usersRepository.Get(request.UserId);
            if (existUser is null) throw new Exception("User Not Found");
            var existProduct = await _masterProductRepository.Get(request.ProductId);
            if (existProduct is null) throw new Exception("Product Not Found");
            if (existProduct.Quantity < request.Quantity) throw new Exception("Not enough quantity");

            var existUserCart = await _userCartRepository.GetByUserIdAndProductIdAsync(request.UserId, request.ProductId);
            if (existUserCart is not null)
            {
                existUserCart.Quantity += request.Quantity;
                existUserCart.TotalPrice = existProduct.Price * existUserCart.Quantity;
                await _userCartRepository.Update(existUserCart);
                result.IsSuccess = true;
            }
            else
            {
                Entities.UserCart userCart = request.Adapt<Entities.UserCart>();
                userCart.TotalPrice = existProduct.Price * request.Quantity;
                var newUserCart = await _userCartRepository.Add(userCart);
                result.IsSuccess = newUserCart is not null;
            }



            result.Message = result.IsSuccess
                ? "Product added to cart successfully"
                : "Failed to add product to cart";
            if (result.IsSuccess)
            {
                existProduct.Quantity -= request.Quantity;
                await _masterProductRepository.Update(existProduct);
                StockProduct stockProduct = request.Adapt<StockProduct>();
                stockProduct.StockTypeId = ConstantsEntity.ADD_TO_CART;
                await _stockProductRepository.Add(stockProduct);
            }
            return result;
        }
    }
}