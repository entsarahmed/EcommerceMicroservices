using AutoMapper;
using Catalog.Application.CQRS.Queries;
using Catalog.Application.ResponseDtos;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.CQRS.Handlers.Queries;
public class GetProductsByBrandQueryHandler : IRequestHandler<GetProductsByBrandQuery, IList<ProductResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductsByBrandQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper=mapper;
        _productRepository=productRepository;
    }
   

    public async Task<IList<ProductResponseDto>> Handle(GetProductsByBrandQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllProductsByBrand(request.BrandName);
        var productResponseList = _mapper.Map<IList<ProductResponseDto>>(products);
        return productResponseList;
    }
}
