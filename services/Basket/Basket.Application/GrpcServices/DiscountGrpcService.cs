using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Basket.Application.GrpcServices
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _discountGrpcClient;
        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient discountgrpcClient)
        {
            _discountGrpcClient = discountgrpcClient;  
        }
        public async Task<CouponModel> GetDiscount(string productName) {
            var request = new GetDiscountRequest() {ProductName =productName };
            return await _discountGrpcClient.GetDiscountAsync(request);
        
        }
    }
}
