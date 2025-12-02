using AutoMapper;
using Catalog.Application.ResponseDtos;
using Catalog.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Application.Mappers;
public class CatalogMappingProfile: Profile
{
    public CatalogMappingProfile()
    {
        // ReverseMap ==> Convert from ProductBrand to BrandResponseDto or BrandResponseDto to ProductBrand
        CreateMap<ProductBrand, BrandResponseDto>().ReverseMap();
    }
}
