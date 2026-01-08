using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories;

public class CatalogRepository : IProductRepository, IBrandRepository, ITypeRepository
{
    private readonly ICatalogContext _context;

    public CatalogRepository(ICatalogContext context)
    {
        _context = context;
    }
    public async Task<Pagination<Product>> GetAllProducts(CatalogSpecParams catalogSpecParams)
    {
        // Create Initialization for Filter
        var builder = Builders<Product>.Filter;
        var filter = builder.Empty;
        // check search or BrandId no Empty
        if (!string.IsNullOrEmpty(catalogSpecParams.Search))
        {
            filter = filter & builder.Where(p => p.Name!.ToLower().Contains(catalogSpecParams.Search.ToLower()));
        }
        if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
        {
            var brandFilter = builder.Eq(p => p.Brand!.Id, catalogSpecParams.BrandId);
            filter &= brandFilter;
        }
        if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
        {
            var typeFilter = builder.Eq(p => p.Type!.Id, catalogSpecParams.TypeId);
            filter &= typeFilter;
        }
        var TotalItems = await _context.Products.CountDocumentsAsync(filter);
        var Data = await DataFilter(catalogSpecParams, filter);

        // p => true == return All product
        return new Pagination<Product>(
            catalogSpecParams.PageIndex,
            catalogSpecParams.PageSize,
            (int)TotalItems,
            Data
            );

    }

    public async Task<Product> GetProductById(string id)
    {
        //I want product throw  id 
        return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<Product>> GetAllProductsByBrand(string name)
    {
        return await _context.Products.Find(p => p.Brand!.Name == name).ToListAsync();
    }
    public async Task<IEnumerable<Product>> GetAllProductsByName(string name)
    {
        return await _context.Products.Find(p => p.Name == name).ToListAsync();
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await _context.Products.InsertOneAsync(product);
        return product;
    }
    public async Task<bool> UpdateProduct(Product product)
    {
        var UpdatedProduct = await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
        return UpdatedProduct.IsAcknowledged && UpdatedProduct.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
        var DeletedProduct = await _context.Products.DeleteOneAsync(p => p.Id == id);
        //IsAcknowledge ==> MongoDb understand request delete and run 
        return DeletedProduct.IsAcknowledged && DeletedProduct.DeletedCount > 0;
    }
    public async Task<IEnumerable<ProductBrand>> GetAllBrands()
    {
        return await _context.Brands.Find(b => true).ToListAsync();
    }

    public async Task<IEnumerable<ProductType>> GetAllTypes()
    {
        return await _context.Types.Find(t => true).ToListAsync();
    }
    private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
    {
        var sortDefn = Builders<Product>.Sort.Ascending("Name");
        if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
        {
            switch (catalogSpecParams.Sort)
            {
                case "priceAsc":
                    sortDefn = Builders<Product>.Sort.Ascending(p => p.Price);
                    break;
                case "priceDesc":
                    sortDefn = Builders<Product>.Sort.Descending(p => p.Price);
                    break;
                default:
                    sortDefn = Builders<Product>.Sort.Ascending(p => p.Name);
                    break;
            }
        }
            return await _context.Products.Find(filter)
                .Sort(sortDefn)
                .Skip(catalogSpecParams.PageSize * (catalogSpecParams.PageIndex - 1))
                .Limit(catalogSpecParams.PageSize)
                .ToListAsync();
        
    }
}

