using AutoMapper;
using Discount.Application.Commands;
using Discount.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discount.Application.Handlers.Commands
{
    public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, bool>
    {
        private readonly IDiscountRepository _discountRepository;
      

        public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
            
        }
        public Task<bool> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var deleted = _discountRepository.DeleteDiscount(request.ProductName);
            return deleted;
        }
    }
}
