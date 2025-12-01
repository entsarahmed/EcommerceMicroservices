using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Infrastructure.Data.Contexts;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories;
public class CatalogRepository : IProductRepository,IBrandRepository, ITypeRepository
{
    private readonly ICatalogContext _context;

    public CatalogRepository(ICatalogContext context)
    {
        _context=context;
    }
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        // p => true == return All product
        return await _context.Products.Find(p => true).ToListAsync();

    }
    
    public async Task<Product> GetProductById(string id)
    {
        //I want product throw  id 
        return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
    public async Task<IEnumerable<Product>> GetAllProductsByBrand(string name)
    {
        return await _context.Products.Find(p => p.Brand.Name == name).ToListAsync();
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
    var UpdatedProduct= await _context.Products.ReplaceOneAsync(p => p.Id ==product.Id, product);
        return UpdatedProduct.IsAcknowledged && UpdatedProduct.ModifiedCount > 0;
    }

    public async Task<bool> DeleteProduct(string id)
    {
       var DeletedProduct = await _context.Products.DeleteOneAsync(p => p.Id ==id);
    //IsAchknowledge ==> MongoDb understand request delete and run 
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
}
