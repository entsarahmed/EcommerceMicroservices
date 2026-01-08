using AutoMapper;
using Catalog.Application.CQRS.Queries;
using Catalog.Application.ResponseDtos;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.CQRS.Handlers.Queries;
public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Pagination<ProductResponseDto>>//GetAllProductsQuery is request ////////IList<ProductResponseDto>  ---this is type of Response
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper=mapper;
        _productRepository=productRepository;
    }
    public async Task<Pagination<ProductResponseDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var ProductList = await _productRepository.GetAllProducts(request.Spec);
        var ProductResponseList = _mapper.Map<Pagination<ProductResponseDto>>(ProductList);
        return ProductResponseList;
    }
}
