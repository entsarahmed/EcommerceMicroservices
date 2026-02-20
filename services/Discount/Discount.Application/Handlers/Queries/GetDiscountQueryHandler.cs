using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using Microsoft.Extensions.Logging;

public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<GetDiscountQueryHandler> _logger;

    public GetDiscountQueryHandler(
        IDiscountRepository discountRepository,
        ILogger<GetDiscountQueryHandler> logger)
    {
        _discountRepository = discountRepository;
        _logger = logger;
    }

    public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
    {
        var coupon = await _discountRepository.GetDiscount(request.ProductName);

        if (coupon == null)
            throw new RpcException(new Status(StatusCode.NotFound,
                $"Discount for ProductName = {request.ProductName} is not Found"));

        var couponModel = new CouponModel
        {
            Id = coupon.Id,
            Amount = coupon.Amount,
            Description = coupon.Description,
            ProductName = coupon.ProductName
        };

        _logger.LogInformation("Coupon for {ProductName} is fetched", request.ProductName);

        return couponModel;
    }
}
