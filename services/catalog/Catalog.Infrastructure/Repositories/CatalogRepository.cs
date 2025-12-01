using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories;
public class CatalogRepository : IProductRepository,IBrandRepository, ITypeRepository
{
    public Task<Product> CreateProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProduct(string id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllProducts()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllProductsByBrand(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllProductsByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductType>> GetAllTypes()
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProduct(Product product)
    {
        throw new NotImplementedException();
    }
}
