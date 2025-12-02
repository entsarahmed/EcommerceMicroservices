using AutoMapper;
using Catalog.Application.CQRS.Queries;
using Catalog.Application.ResponseDtos;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.CQRS.Handlers.Queries;
public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, IList<BrandResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IBrandRepository _brandRepository;

    public GetAllBrandsQueryHandler(IMapper mapper, IBrandRepository brandRepository)
    {
        _mapper=mapper;
        _brandRepository=brandRepository;
    }
    //What is Cancellation Token  ==> Object send with any progress Asynchronous  => if make cancel for Progress like user make request after it's close page ==> Cancellation Token Know stop progress treatment
    public async Task<IList<BrandResponseDto>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
    {
        var BrandList = await _brandRepository.GetAllBrands();
        var BrandResponseList = _mapper.Map<IList<ProductBrand>, IList<BrandResponseDto>>(BrandList.ToList());
        return BrandResponseList;
    }
}
