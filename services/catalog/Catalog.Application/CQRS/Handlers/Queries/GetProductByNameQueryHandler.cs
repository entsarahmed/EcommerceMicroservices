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
public class GetProductByNameQueryHandler : IRequestHandler<GetProductByNameQuery, IList<ProductResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;

    public GetProductByNameQueryHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper=mapper;
        _productRepository=productRepository;
    }
    
   public async Task<IList<ProductResponseDto>> Handle(GetProductByNameQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetAllProductsByName(request.Name);
        var productResponse = _mapper.Map<IList<ProductResponseDto>>(product);
        return productResponse;
    }
}
