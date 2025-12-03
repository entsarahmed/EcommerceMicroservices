using Catalog.Application.ResponseDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.CQRS.Queries;
public class GetProductsByBrandQuery:IRequest<IList<ProductResponseDto>>
{
    public string BrandName { get; set; }
    public GetProductsByBrandQuery(string brandName)
    {
        BrandName = brandName;
    }
}
