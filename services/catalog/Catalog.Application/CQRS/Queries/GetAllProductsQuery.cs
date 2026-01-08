using Catalog.Application.ResponseDtos;
using Catalog.Core.Specs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.CQRS.Queries;
public class GetAllProductsQuery : IRequest<Pagination<ProductResponseDto>>//Type of Response
{
    public CatalogSpecParams Spec { get; set; }
    public GetAllProductsQuery(CatalogSpecParams spec)
    {
        Spec = spec;
        
    }
}
