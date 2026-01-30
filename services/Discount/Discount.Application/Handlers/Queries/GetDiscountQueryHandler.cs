using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Handlers.Queries
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly ILogger _logger;

        public GetDiscountQueryHandler(IDiscountRepository discountRepository, ILogger logger)
        {
            _discountRepository = discountRepository;
            _logger = logger;
        }
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await _discountRepository.GetDiscount(request.ProductName);
            if (coupon == null)
                throw new RpcException(new Status(StatusCode.NotFound,$"Discount for ProductName = {request.ProductName} is not Found"));
            var couponModel = new CouponModel { 
            Id = coupon.Id,
            Amount = coupon.Amount,
            Description = coupon.Description,
            ProductName = coupon.ProductName
            };
            _logger.LogInformation($"Coupon for the {request.ProductName} is fethed");
            return couponModel;
        }
        
    }
}
